using Models.MainDb;
using Database.MainDb;

namespace Services.MainDb
{
    /// <summary>接口</summary>
    public interface ITransactionService : IBaseService<TransactionModel>
    {
    }

    /// <summary>服务</summary>
    public class TransactionService : BaseService<TransactionModel>, ITransactionService
    {
        /// <summary>服务</summary>
        public TransactionService(ITransactionStorage storage) : base(storage)
        { }
    }
}