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
    [Table("Transaction")]
    public class TransactionModel
    {
        /// <summary></summary>
        public int Id { get; set; }

        /// <summary></summary>
        public string OrderNo { get; set; }

        /// <summary></summary>
        public decimal Price { get; set; }

        /// <summary></summary>
        public string Status { get; set; }

        /// <summary></summary>
        public long Timestamp { get; set; }

        /// <summary></summary>
        public string Username { get; set; }
    }
}
