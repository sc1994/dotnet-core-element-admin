using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Domain.Entities.ElementAdminDb;
using ElementAdmin.Domain.ObjVal;
using ElementAdmin.Infrastructure.Common;
using ElementAdmin.Infrastructure.Repositories.ElementAdminDb;
using Valit;

namespace ElementAdmin.Domain.Aggregate
{
    public class RoleAggRoot : BaseResult
    {
        public RoleAggRoot() {}

        private readonly IRolesRoutesStorage _rolesRoutes;
        private readonly IRolesStorage _roles;
        private readonly IRoutesStorage _routes;

        public RoleAggRoot(IRolesRoutesStorage rolesRoutes, IRoutesStorage routes, RolesEntity model)
        {
            _rolesRoutes = rolesRoutes;
            _routes = routes;

            Name = model.Name;
            RoleKey = model.RoleKey;
            Description = model.Description;

            InitRouteKeys().Wait();
        }

        public RoleAggRoot(IRolesRoutesStorage rolesRoutes, IRolesStorage roles, string key)
        {
            _rolesRoutes = rolesRoutes;
            _roles = roles;

            RoleKey = key;
        }

        public RoleAggRoot(IRolesRoutesStorage rolesRoutes, IRolesStorage roles, RoleAggRoot model)
        {
            _rolesRoutes = rolesRoutes;
            _roles = roles;

            Name = model.Name;
            RoleKey = model.RoleKey;
            Description = model.Description;
            RouteKeys = model.RouteKeys;
        }

        public RoleAggRoot(IRolesRoutesStorage rolesRoutes, IRolesStorage roles, IRoutesStorage routes, RoleAggRoot model)
        {
            _rolesRoutes = rolesRoutes;
            _roles = roles;
            _routes = routes;

            Name = model.Name;
            RoleKey = model.RoleKey;
            Description = model.Description;
            RouteKeys = model.RouteKeys;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色Key
        /// </summary>
        public string RoleKey { get; set; }

        /// <summary>
        /// 角色可访问路由
        /// </summary>
        public IEnumerable<string> RouteKeys { get; set; }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public async Task<Result> AddAsync()
        {
            if (!ValitRules())return Bad("参数不全");

            // 验证角色重复
            var first = await _roles.FirstOrDefaultAsync(x => x.RoleKey == RoleKey);
            if (first != null)return Bad("已存在的角色");
            //添加角色
            var result1 = await _roles.AddAsync(Mapper.ToMap<RolesEntity>(this));
            if (!result1.done)return Bad("更新数据异常");
            // 添加路由和角色之间的关系
            var addRoleRoutes = RouteKeys.Select(x => new RolesRoutesEntity
            {
                RoleKey = RoleKey,
                    RouteKey = x
            });
            await _rolesRoutes.AddRangeAsync(addRoleRoutes);

            return Ok();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        public async Task<Result> UpdateAsync()
        {
            if (!ValitRules())return Bad("参数不全");

            // 验证角色是否存在
            var first = await _roles.FirstOrDefaultAsync(x => x.RoleKey == RoleKey);
            if (first == null)return Bad("不存在的角色Key");

            // 验证是否需要更新
            var rules = ValitRules<RolesEntity>
                .Create()
                .Ensure(x => x.Description, _ => _.IsEqualTo(Description))
                .Ensure(x => x.Name, _ => _.IsEqualTo(Name))
                .Ensure(x => x.RoleKey, _ => _.IsEqualTo(RoleKey))
                .For(first)
                .Validate();
            if (!rules.Succeeded)
            {
                await _roles.UpdateAsync(Mapper.ToMap<RolesEntity>(this));
            }

            var roleRoutes = await _rolesRoutes.FindAsync(x => x.RoleKey == RoleKey);
            var deleteRouteKeys = roleRoutes.Select(x => x.RouteKey).Except(RouteKeys);
            var deleteModels = roleRoutes.Where(x => deleteRouteKeys.Contains(x.RouteKey));
            await _rolesRoutes.RemoveRangeAsync(deleteModels);

            var addRouteKeys = RouteKeys.Except(roleRoutes.Select(x => x.RouteKey));
            var addModels = addRouteKeys.Select(x => new RolesRoutesEntity
            {
                RoleKey = RoleKey,
                    RouteKey = x
            });

            await _rolesRoutes.AddRangeAsync(addModels);

            return Ok();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public async Task<Result> DeleteAsync()
        {
            var first = await _roles.FirstOrDefaultAsync(x => x.RoleKey == RoleKey);
            if (first == null)return Bad("错误的角色Key");

            var r1 = await _roles.RemoveAsync(first);
            if (r1 < 1)return Bad("删除失败");

            var rolesRoutes = await _rolesRoutes.FindAsync(x => x.RoleKey == RoleKey);
            await _rolesRoutes.RemoveRangeAsync(rolesRoutes);
            return Ok();
        }

        private async Task InitRouteKeys()
        {
            var sourd = await _routes.FindAsync(x => !string.IsNullOrWhiteSpace(x.Name));
            var routes = await _rolesRoutes.FindAsync(x => x.RoleKey == RoleKey);
            var pKeys = sourd.Select(x => x.ParentKey);

            RouteKeys = routes.Where(x => !pKeys.Contains(x.RouteKey))
                .Select(x => x.RouteKey);
        }

        private bool ValitRules()
        {
            // 验证参数
            var rules = ValitRules<RoleAggRoot>
                .Create()
                .Ensure(x => x.RoleKey, _ => _.Required().MinLength(1))
                .Ensure(x => x.Description, _ => _.Required().MinLength(1))
                .Ensure(x => x.Name, _ => _.Required().MinLength(1))
                .For(this)
                .Validate();
            return rules.Succeeded;
        }
    }
}