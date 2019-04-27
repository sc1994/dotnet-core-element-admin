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
    [Table("UserInfo")]
    public class UserInfoModel
    {
        /// <summary>主键</summary>
        public int Id { get; set; }

        /// <summary></summary>
        public string RolesString { get; set; }

        /// <summary></summary>
        public string Token { get; set; }

        /// <summary></summary>
        public string Introduction { get; set; }

        /// <summary></summary>
        public string Avatar { get; set; }

        /// <summary></summary>
        public string Name { get; set; }

        /// <summary></summary>
        public string Username { get; set; }

        /// <summary></summary>
        public string Password { get; set; }
    }
}
