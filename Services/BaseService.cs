using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Database;

namespace Services
{
    /// <summary>数据基类接口</summary>
    public interface IBaseService<TModel>
        where TModel : class
    {
        /// <summary>新增</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<(bool done, TModel after)> AddAsync(TModel model);

        /// <summary>批量新增</summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync(IEnumerable<TModel> list);

        /// <summary>删除</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> RemoveAsync(TModel model);

        /// <summary>批量删除</summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<int> RemoveRangeAsync(IEnumerable<TModel> list);

        /// <summary>更新</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<(bool done, TModel after)> UpdateAsync(TModel model);

        /// <summary>批量更新</summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<int> UpdateRangeAsync(IEnumerable<TModel> list);

        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        Task<TModel> FirstOrDefaultAsync(Expression<Func<TModel, bool>> predicate);

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<List<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate, int index = 0, int size = 0);

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<List<TModel>> FindAsync(TModel predicate, int index = 0, int size = 0);
    }

    /// <summary>数据基类</summary>
    public class BaseService<TModel> : IBaseService<TModel>
        where TModel : class
    {
        private readonly IBaseStorage<TModel> _storage;

        /// <summary>数据基类</summary>
        protected BaseService(IBaseStorage<TModel> storage)
        {
            _storage = storage;
        }

        /// <summary>新增</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool done, TModel after)> AddAsync(TModel model)
            => await _storage.AddAsync(model);

        /// <summary>批量新增</summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> AddRangeAsync(IEnumerable<TModel> list)
            => await _storage.AddRangeAsync(list);

        /// <summary>删除</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> RemoveAsync(TModel model)
            => await _storage.RemoveAsync(model);

        /// <summary>批量删除</summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> RemoveRangeAsync(IEnumerable<TModel> list)
            => await _storage.RemoveRangeAsync(list);

        /// <summary>更新</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool done, TModel after)> UpdateAsync(TModel model)
            => await _storage.UpdateAsync(model);

        /// <summary>批量更新</summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> UpdateRangeAsync(IEnumerable<TModel> list)
            => await _storage.UpdateRangeAsync(list);

        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        public async Task<TModel> FirstOrDefaultAsync(Expression<Func<TModel, bool>> predicate)
            => await _storage.FirstOrDefaultAsync(predicate);

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate, int index = 0, int size = 0)
            => await _storage.FindAsync(predicate, index, size);

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<List<TModel>> FindAsync(TModel predicate, int index = 0, int size = 0)
            => await _storage.FindAsync(predicate, index, size);
    }
}