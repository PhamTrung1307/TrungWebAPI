using Core.Domain.Royalty;
using Core.Models;
using Core.Models.Royalty;
using Core.SeedWorks;


namespace Core.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
        Task<PageResult<TransactionDTO>> GetAllPaging(string? userName, int fromMonth, int fromYear, int toMonth, int toYear, int pageIndex = 1, int pageSize = 10);
    }
}
