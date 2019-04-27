// =============系统自动生成=============
// 时间：2019/4/27 16:16
// 备注：表字段对应的数据模型。请勿在此文件中变动代码。
// =============系统自动生成=============
// ReSharper disable InconsistentNaming

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.MainDb
{
    /// <summary></summary>
    [Table("RoleRoute")]
    public class RoleRouteModel
    {
        /// <summary>主键</summary>
        public int Id { get; set; }

        /// <summary>角色Id</summary>
        public int RoleId { get; set; }

        /// <summary>路由Id</summary>
        public int RouteId { get; set; }
    }
}
