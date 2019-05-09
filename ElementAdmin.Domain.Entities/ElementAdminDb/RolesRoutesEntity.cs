// =============系统自动生成=============
// 时间：2019/5/9 14:25
// 备注：表字段对应的数据模型。请勿在此文件中变动代码。
// =============系统自动生成=============
// ReSharper disable InconsistentNaming

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementAdmin.Domain.Entities.ElementAdminDb
{
    /// <summary>路由角色多对多关系表</summary>
    [Table("RolesRoutes")]
    public class RolesRoutesEntity
    {
        /// <summary>主键</summary>
        public int Id { get; set; }

        /// <summary>角色Key</summary>
        public string RoleKey { get; set; }

        /// <summary>路由Key</summary>
        public string RouteKey { get; set; }
    }
}
