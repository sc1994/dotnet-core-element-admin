// =============系统自动生成=============
// 时间：2019/4/27 10:00
// 备注：表字段对应的数据模型。请勿在此文件中变动代码。
// =============系统自动生成=============
// ReSharper disable InconsistentNaming

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.MainDb
{
    /// <summary></summary>
    [Table("Routes")]
    public class RoutesModel
    {
        /// <summary></summary>
        public int Id { get; set; }

        /// <summary></summary>
        public string Path { get; set; }

        /// <summary></summary>
        public string Component { get; set; }

        /// <summary></summary>
        public string Hidden { get; set; }

        /// <summary></summary>
        public string RoleKey { get; set; }

        /// <summary></summary>
        public int RouteId { get; set; }
    }
}
