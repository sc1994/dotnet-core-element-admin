using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.MainDb;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticleController 
    {
        [HttpGet("list")]
        public ResultModel<PageModel<ArticleModel>> ArticleList(int page, int limit)
        {
            return new ResultModel<PageModel<ArticleModel>>
            {
                Code = 20000,
                Data = new PageModel<ArticleModel>
                {
                    Items = new List<ArticleModel>
                    {
                        new ArticleModel
                        {
                            Author = "Author",
                            CommentDisabled = "CommentDisabled",
                            Content = "Content",
                            ContentShort = "ContentShort",
                            //DisplayTime = DateTime.Now.ToString(), todo 
                            Forecast = 1.1M,
                            Id = 1,
                            ImageUri = "ImageUri",
                            Importance = 5,
                            Pageviews = 1,
                            //Platforms = new List<string>{ "Platforms1", "Platforms2" }, todo
                            Remark = "Remark",
                            Reviewer = "Reviewer",
                            Status = "Status",
                            Timestamp = (DateTime.Now - DateTime.MinValue).TotalSeconds.ToString(CultureInfo.InvariantCulture),
                            Title = "Title",
                            Type = "Type"
                        }
                    },
                    Total = 1
                }
            };
        }

        public ResultModel<string> CreateArticle(ArticleModel model)
        {
            return new ResultModel<string>
            {
                Code = 20000,
                Data = "success"
            };
        }


        public ResultModel<string> UpdateArticle(ArticleModel model)
        {
            return new ResultModel<string>
            {
                Code = 20000,
                Data = "success"
            };
        }
    }



}