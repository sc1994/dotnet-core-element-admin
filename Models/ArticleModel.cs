using System.Collections.Generic;

namespace Models
{
    public partial class ArticleModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary> 
        /// 
        /// </summary>
        public int Importance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Author { get; set; }
    }

    public partial class ArticleModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Reviewer { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ContentShort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Forecast { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CommentDisabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Pageviews { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ImageUri { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> Platforms { get; set; }
    }
}
