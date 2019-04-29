// =============系统自动生成=============
// 时间：2019/4/29 15:11
// 备注：表字段对应的数据模型。请勿在此文件中变动代码。
// =============系统自动生成=============
// ReSharper disable InconsistentNaming

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.MainDb
{
    /// <summary></summary>
    [Table("Roles")]
    public class RolesModel
    {
        /// <summary>描述</summary>
        public string Description { get; set; }

        /// <summary>角色</summary>
        public string Key { get; set; }

        /// <summary>名称</summary>
        public string Name { get; set; }
    }
}
