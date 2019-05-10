using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Domain.Context;
using ElementAdmin.Domain.Entities.ElementAdminDb;
using ElementAdmin.Domain.ObjVal;
using ElementAdmin.Infrastructure.Repositories.ElementAdminDb;
using Microsoft.EntityFrameworkCore.Internal;

namespace ElementAdmin.Domain.Factories
{
    public interface IToolsFactory
    {
        /// <summary>
        /// 初始化路由数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<Result> InitRouteDataAsync(IEnumerable<InitRouteDataContext> context);

        /// <summary>
        /// 初始化用户数据
        /// </summary>
        /// <returns></returns>
        Task<Result> InitUserDataAsync();
    }

    public class ToolsFactory : BaseResult, IToolsFactory
    {
        private readonly IRoutesStorage _routes;
        private readonly IUserInfoStorage _user;

        public ToolsFactory(IRoutesStorage routes, IUserInfoStorage user)
        {
            _routes = routes;
            _user = user;
        }

        public async Task<Result> InitRouteDataAsync(IEnumerable<InitRouteDataContext> context)
        {
            var source = await _routes.FindAsync(x => !string.IsNullOrWhiteSpace(x.Name));
            var newData = new List<RoutesEntity>();
            foreach (var item in context)
            {
                InitChildrens("", item, ref newData);
            }

            var removeKeys = source.Select(x => x.RouteKey).Except(newData.Select(x => x.RouteKey));
            await _routes.RemoveRangeAsync(source.Where(x => removeKeys.Contains(x.RouteKey)));

            var addKeys = newData.Select(x => x.RouteKey).Except(source.Select(x => x.RouteKey));
            await _routes.AddRangeAsync(newData.Where(x => addKeys.Contains(x.RouteKey)));

            var updateKeys = source.Select(x => x.RouteKey).Intersect(newData.Select(x => x.RouteKey));
            await _routes.UpdateRangeAsync(source.Where(x => updateKeys.Contains(x.RouteKey)));

            return Ok();
        }

        public async Task<Result> InitUserDataAsync()
        {
            var data = new List<UserInfoEntity>
            {
                new UserInfoEntity
                {
                Name = "管理员一号",
                Avatar = "http://img0.imgtn.bdimg.com/it/u=2426955815,3090482659&fm=214&gp=0.jpg",
                Introduction = "我是管理员哈哈哈哈",
                Password = "111111",
                RolesString = "admin,dev",
                Token = Guid.NewGuid().ToString(),
                Username = "admin"
                }
            };
            await _user.AddRangeAsync(data);
            return Ok();
        }

        private Result InitChildrens(
            string pKey,
            InitRouteDataContext item,
            ref List<RoutesEntity> newData)
        {

            if (item.Children?.Any() ?? false)
            {
                foreach (var x in item.Children)
                {
                    InitChildrens(item.Name, x, ref newData);
                }
            }
            if (!string.IsNullOrWhiteSpace(item.Name))
            {
                newData.Add(new RoutesEntity
                {
                    RouteKey = item.Name,
                        ParentKey = pKey,
                        Name = item.Meta?.Title,
                        Sort = item.Sort ?? newData.Count
                });
            }

            return Ok();
        }
    }
}