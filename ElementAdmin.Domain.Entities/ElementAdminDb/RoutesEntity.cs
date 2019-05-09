// =============系统自动生成=============
// 时间：2019/5/9 17:12
// 备注：表字段对应的数据模型。请勿在此文件中变动代码。
// =============系统自动生成=============
// ReSharper disable InconsistentNaming

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementAdmin.Domain.Entities.ElementAdminDb
{
    /// <summary>路由表</summary>
    [Table("Routes")]
    public class RoutesEntity
    {
        /// <summary>名称</summary>
        public string Name { get; set; }

        /// <summary>父级Key</summary>
        public string ParentKey { get; set; }

        /// <summary>路由Key</summary>
        public string RouteKey { get; set; }

        /// <summary></summary>
        public int Sort { get; set; }
    }
}
