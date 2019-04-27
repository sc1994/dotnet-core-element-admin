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
    [Table("Article")]
    public class ArticleModel
    {
        /// <summary></summary>
        public int Id { get; set; }

        /// <summary></summary>
        public int Importance { get; set; }

        /// <summary></summary>
        public string Remark { get; set; }

        /// <summary></summary>
        public string Timestamp { get; set; }

        /// <summary></summary>
        public string Title { get; set; }

        /// <summary></summary>
        public string Status { get; set; }

        /// <summary></summary>
        public string Type { get; set; }

        /// <summary></summary>
        public string Author { get; set; }

        /// <summary></summary>
        public string CommentDisabled { get; set; }

        /// <summary></summary>
        public string Content { get; set; }

        /// <summary></summary>
        public string ContentShort { get; set; }

        /// <summary></summary>
        public DateTime DisplayTime { get; set; }

        /// <summary></summary>
        public decimal Forecast { get; set; }

        /// <summary></summary>
        public string ImageUri { get; set; }

        /// <summary></summary>
        public int Pageviews { get; set; }

        /// <summary></summary>
        public string Platforms { get; set; }

        /// <summary></summary>
        public string Reviewer { get; set; }
    }
}
