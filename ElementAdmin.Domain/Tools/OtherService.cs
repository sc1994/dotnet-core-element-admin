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
using ElementAdmin.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using static ElementAdmin.Application.Model.ApiResponse;

namespace ElementAdmin.Domain.Tools
{
    public partial class ToolService : IToolService
    {
        private readonly IRouteRepository _route;
        private readonly IHubContext<StressTestHub> _hubContext;
        private readonly IConfiguration _config;

        public ToolService(
            IRouteRepository route,
            IHubContext<StressTestHub> hubContext,
            IConfiguration config)
        {
            _route = route;
            _hubContext = hubContext;
            _config = config;
        }

        public async Task<ApiResponse> InitRouteDataAsync(RouteModel[] routes)
        {
            var routeEntities = new List<RouteEntity>();
            foreach (var item in routes)
            {
                FlatChildrenData("", item, ref routeEntities);
            }

            // 全量的删除添加
            var all = await _route.WhereAsync(x => x.Id > 0);
            // 删除
            await _route.RemoveRangeAsync(all, true);
            // 添加
            await _route.AddRangeAsync(routeEntities);
            // 保存
            var rows = await _route.SaveChangesAsync();

            return await Task.FromResult(Ok(rows));
        }

        private void FlatChildrenData(string pKey, RouteModel item, ref List<RouteEntity> result)
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
