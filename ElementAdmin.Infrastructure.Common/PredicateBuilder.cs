using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ElementAdmin
{
    /// <summary>Enables the efficient, dynamic composition of query predicates.</summary>
    public static class PredicateBuilder
    {
        /// <summary>Combines the first predicate with the second using the logical "and".</summary>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
            => first.Compose(second, Expression.AndAlso);

        /// <summary>Combines the first predicate with the second using the logical "or".</summary>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
            => first.Compose(second, Expression.OrElse);

        /// <summary>Combines the first expression with the second using the specified merge function.</summary>
        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
    }

    /// <summary></summary>
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        /// <summary></summary>
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary></summary>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            => new ParameterRebinder(map).Visit(exp);

        /// <summary></summary>
        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (_map.TryGetValue(p, out var replacement))
                p = replacement;
            return base.VisitParameter(p);
        }
    }
}


