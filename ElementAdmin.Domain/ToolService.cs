using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ElementAdmin.Application.Interface;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Tools;
using ElementAdmin.Domain.Entity.ElementAdmin;
using ElementAdmin.Domain.Interface.ElementAdmin;
using static ElementAdmin.Application.Model.ApiResponse;

namespace ElementAdmin.Domain
{
    public class ToolService : IToolService
    {
        private readonly IRouteRepository _route;

        public ToolService(IRouteRepository route)
        {
            _route = route;
        }

        public async Task<ApiResponse> InitRouteDataAsync(InitRouteDataModel[] routes)
        {
            var routeEntities = new List<RouteEntity>();
            foreach (var item in routes)
            {
                FlatChildrenData("", item, ref routeEntities);
            }

            var old = (await _route.WhereAsync(x => !x.IsDelete)).ToArray();
            // 删除
            var removeKeys = old.Select(x => x.RouteKey).Except(routeEntities.Select(x => x.RouteKey));
            await _route.RemoveRangeAsync(old.Where(x => removeKeys.Contains(x.RouteKey)), true);
            // 添加
            var addKeys = routeEntities.Select(x => x.RouteKey).Except(old.Select(x => x.RouteKey));
            await _route.AddRangeAsync(routeEntities.Where(x => addKeys.Contains(x.RouteKey)));
            // 更新
            var updateKeys = old.Select(x => x.RouteKey).Intersect(routeEntities.Select(x => x.RouteKey));
            await _route.UpdateRangeAsync(old.Where(x => updateKeys.Contains(x.RouteKey)));
            // 保存
            var rows = await _route.SaveChangesAsync();

            return await Task.FromResult(Ok(rows));
        }

        private void FlatChildrenData(string pKey, InitRouteDataModel item, ref List<RouteEntity> result)
        {
            if (item.Children?.Any() ?? false)
            {
                foreach (var x in item.Children)
                {
                    FlatChildrenData(item.Name, x, ref result);
                }
            }

            if (string.IsNullOrWhiteSpace(item.Name)) return;

            result.Add(new RouteEntity
            {
                Name = item.Meta?.Title ?? item.Name,
                ParentRouteKey = pKey,
                RouteKey = item.Name,
                Sort = item.Sort ?? result.Count
            });
        }

        public ApiResponse GetEntities()
        {
            var allClass = Assembly.Load("ElementAdmin.Domain.Entity").GetTypes();
            var types = allClass.Where(x => x.IsClass && x.IsPublic)
                                .GroupBy(x => x.Namespace)
                                .ToDictionary(x => x.Key.Split('.')[x.Key.Split('.').Length - 1], x => x.Select(s => s.Name))
                                .Where(x => x.Key != "Entity"); // 筛选掉基类
            return Ok(types.ToArray());
        }

        public ApiResponse InitEntities(string[] entities)
        {
            // todo 环境验证，只有开发环境才可以调用此方法
            var allClass = Assembly.Load("ElementAdmin.Domain.Entity").GetTypes();
            var types = allClass.Where(x => entities.Contains(x.Name)).ToArray();
            if (!types.Any()) return Bad("没有需要生成的Entity");

            foreach (var type in types)
            {
                var namespaceSplit = type.Namespace.Split('.');
                var ending = namespaceSplit[namespaceSplit.Length - 1];
                // 实现---------------
                var path = $@"D:\Other\ElementAdmin3\ElementAdmin.Infrastructure.Repository\{ending}";
                Directory.CreateDirectory(path);
                path += $@"\{type.Name.Trim("Entity".ToCharArray())}Repository.cs";
                var idType = type.GetProperty("Id");
                if (!File.Exists(path))
                {
                    var code =
$@"using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ElementAdmin.Domain.Entity.{ending};
using ElementAdmin.Domain.Interface.{ending};
using ElementAdmin.Infrastructure.Data.Context;

namespace ElementAdmin.Infrastructure.Repository.{ending}
{{
    public class {type.Name.Trim("Entity".ToCharArray())}Repository : Repository<{idType.PropertyType.Name}, {type.Name}, {ending}Context>, I{type.Name.Trim("Entity".ToCharArray())}Repository
    {{
        private readonly {ending}Context _context;

        public {type.Name.Trim("Entity".ToCharArray())}Repository({ending}Context context) : base(context)
        {{
            _context = context;
        }}

        public override async Task<IEnumerable<{type.Name}>> WhereAsync(Expression<Func<{type.Name}, bool>> expression)
        {{
            return await _context.{type.Name}.Where(expression).ToListAsync();
        }}

        public override async Task<{type.Name}> FindAsync(Expression<Func<{type.Name}, bool>> expression)
        {{
            return await _context.{type.Name}.FirstOrDefaultAsync(expression);
        }}
    }}
}}";
                    File.WriteAllText(path, code);
                }
                // 接口---------------
                path = $@"D:\Other\ElementAdmin3\ElementAdmin.Domain.Interface\{ending}";
                Directory.CreateDirectory(path);
                path += $@"\I{type.Name.Trim("Entity".ToCharArray())}Repository.cs";
                if (!File.Exists(path))
                {

                    var code =
$@"using System;
using ElementAdmin.Domain.Entity.ElementAdmin;

namespace ElementAdmin.Domain.Interface.{ending}
{{
    public interface I{type.Name.Trim("Entity".ToCharArray())}Repository : IRepository<{idType.PropertyType.Name}, {type.Name}>
    {{

    }}
}}";
                    File.WriteAllText(path, code);
                }
            }
            return Ok();
        }
    }
}
