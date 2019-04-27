using Models.MainDb;

namespace Database.MainDb
{
    /// <summary>接口</summary>
    public interface ITransactionStorage : IBaseStorage<TransactionModel>
    {
    }

    /// <summary></summary>
    public partial class TransactionStorage : ITransactionStorage
    {
    }
}
