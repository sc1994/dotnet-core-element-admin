// =============系统自动生成=============
// 时间：2019/5/9 14:25
// 备注：表字段对应的数据模型。请勿在此文件中变动代码。
// =============系统自动生成=============
// ReSharper disable InconsistentNaming

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementAdmin.Domain.Entities.ElementAdminDb
{
    /// <summary>角色表</summary>
    [Table("Roles")]
    public class RolesEntity
    {
        /// <summary>描述</summary>
        public string Description { get; set; }

        /// <summary>名称</summary>
        public string Name { get; set; }

        /// <summary>角色Key</summary>
        public string RoleKey { get; set; }
    }
}
