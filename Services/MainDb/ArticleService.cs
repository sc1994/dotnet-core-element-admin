using Models.MainDb;
using Database.MainDb;

namespace Services.MainDb
{
    /// <summary>接口</summary>
    public interface IArticleService : IBaseService<ArticleModel>
    {
    }

    /// <summary>服务</summary>
    public class ArticleService : BaseService<ArticleModel>, IArticleService
    {
        /// <summary>服务</summary>
        public ArticleService(IArticleStorage storage) : base(storage)
        { }
    }
}