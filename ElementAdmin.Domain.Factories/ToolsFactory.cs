using ElementAdmin.Domain.Context;
using ElementAdmin.Domain.Entities.ElementAdminDb;
using ElementAdmin.Domain.ObjVal;
using ElementAdmin.Infrastructure.Repositories.ElementAdminDb;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElementAdmin.Domain.Factories
{
    public interface IToolsFactory
    {
        /// <summary>
        /// 初始化路由数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<Result> InitRouteDataAsync(List<InitRouteDataContext> context);

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

        public async Task<Result> InitRouteDataAsync(List<InitRouteDataContext> context)
        {
            foreach (var item in context)
            {
                await InitChildrens("", item);
            }
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

        private async Task<Result> InitChildrens(string pKey, InitRouteDataContext item)
        {
            var addModel = new List<RoutesEntity>();

            if (item.children?.Any() ?? false)
            {
                foreach (var x in item.children)
                {
                    await InitChildrens(item.name, x);
                }
            }
            addModel.Add(new RoutesEntity
            {
                RouteKey = item.name,
                ParentKey = pKey,
                Name = item.meta?.title
            });
            await _routes.AddRangeAsync(addModel);
            return Ok();
        }
    }
}
