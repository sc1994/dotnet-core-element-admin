using System;
using System.Collections.Generic;
using System.Linq;
using ElementAdmin.Domain.Entity.ElementAdmin;
using ElementAdmin.Infrastructure;

namespace ElementAdmin.Application.Model.Role
{
    public class RoleModel
    {
        public RoleModel() { }

        public RoleModel(
            RoleEntity entity,
            IEnumerable<RoleRouteEntity> roleRoutes,
            IEnumerable<RouteEntity> routes)
        {
            Id = entity.Id;
            RoleKey = entity.RoleKey;
            Name = entity.Name;
            Description = entity.Description;

            RouteKeys = roleRoutes
            .Where(x => x.RoleKey == entity.RoleKey)
            .Where(x => routes.Any(a => a.RouteKey == x.RouteKey))
            .Select(x => x.RouteKey).ToArray();
        }

        public long Id { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        /// <value></value>
        public string RoleKey { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <value></value>
        public string Description { get; set; }

        /// <summary>
        /// 路由权限
        /// </summary>
        /// <value></value>
        public string[] RouteKeys { get; set; }

        /// <summary>
        /// 转到路由实体
        /// </summary>
        /// <returns></returns>
        public RoleEntity ToRoleEntity()
        {
            return new RoleEntity
            {
                Id = Id,
                RoleKey = RoleKey,
                Name = Name,
                Description = Description
            };
        }

        public string VerifyMessgae { get; private set; }

        public bool VerifyAdd(RoleEntity role)
        {
            if (string.IsNullOrWhiteSpace(RoleKey))
            {
                VerifyMessgae = "缺少key";
                return false;
            }
            if (RoleKey.Length < RoleEntity.RoleKeyMinLength || RoleKey.Length > RoleEntity.RoleKeyMaxLength)
            {
                VerifyMessgae = $"Key的长度必须在{RoleEntity.RoleKeyMinLength}到{RoleEntity.RoleKeyMaxLength}之间";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                VerifyMessgae = "缺少名称";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Description))
            {
                VerifyMessgae = "缺少描述";
                return false;
            }
            if (role != null)
            {
                VerifyMessgae = $"已存在【{RoleKey}】角色";
                return false;
            }
            return true;
        }

        public VerifyUpdateEnum VerifyUpdate(RoleEntity role)
        {
            if (role == null)
            {
                VerifyMessgae = "数据不存在";
                return VerifyUpdateEnum.Fail;
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                VerifyMessgae = "缺少名称";
                return VerifyUpdateEnum.Fail;
            }
            if (string.IsNullOrWhiteSpace(Description))
            {
                VerifyMessgae = "缺少描述";
                return VerifyUpdateEnum.Fail;
            }
            if (Name == role.Name &&
            Description == role.Description)
            {
                return VerifyUpdateEnum.UnNeed;
            }
            return VerifyUpdateEnum.Need;
        }

        public static bool VerifyDelete(RoleEntity role)
        {
            if (role == null)
            {
                return false;
            }
            if (role.IsDelete)
            {
                return false;
            }
            return true;
        }
    }
}