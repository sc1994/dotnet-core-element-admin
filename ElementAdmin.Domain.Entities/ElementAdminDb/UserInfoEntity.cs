// =============系统自动生成=============
// 时间：2019/5/9 17:12
// 备注：表字段对应的数据模型。请勿在此文件中变动代码。
// =============系统自动生成=============
// ReSharper disable InconsistentNaming

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementAdmin.Domain.Entities.ElementAdminDb
{
    /// <summary>用户信息表</summary>
    [Table("UserInfo")]
    public class UserInfoEntity
    {
        /// <summary>头像</summary>
        public string Avatar { get; set; }

        /// <summary>主键</summary>
        public int Id { get; set; }

        /// <summary>简介</summary>
        public string Introduction { get; set; }

        /// <summary>名称</summary>
        public string Name { get; set; }

        /// <summary>密码</summary>
        public string Password { get; set; }

        /// <summary>所属角色</summary>
        public string RolesString { get; set; }

        /// <summary>token</summary>
        public string Token { get; set; }

        /// <summary>用户名</summary>
        public string Username { get; set; }
    }
}
