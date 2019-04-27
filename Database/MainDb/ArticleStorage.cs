using Models.MainDb;

namespace Database.MainDb
{
    /// <summary>接口</summary>
    public interface IArticleStorage : IBaseStorage<ArticleModel>
    {
    }

    /// <summary></summary>
    public partial class ArticleStorage : IArticleStorage
    {
    }
}
