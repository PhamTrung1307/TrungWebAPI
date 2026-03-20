using AutoMapper;
using Core.Domain.Royalty;
using Core.Models;
using Core.Models.Royalty;
using Core.Repositories;
using Core.SeedWorks;
using Data.SeedWorks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction, Guid>, ITransactionRepository
    {
        private readonly IMapper _mapper;

        public TransactionRepository(IDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Add(System.Transactions.Transaction entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<System.Transactions.Transaction> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<System.Transactions.Transaction> Find(Expression<Func<System.Transactions.Transaction, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResult<TransactionDTO>> GetAllPaging(string? userName, int fromMonth, int fromYear, int toMonth, int toYear, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Transactions.AsQueryable();
            if (!string.IsNullOrWhiteSpace(userName))
            {
                query = query.Where(x => x.ToUserName.Contains(userName));
            }
            if (fromMonth > 0 && fromYear > 0)
            {
                query = query.Where(x => x.DateCreated.Date.Month >= fromMonth && x.DateCreated.Year >= fromYear);
            }
            if (toMonth > 0 && toYear > 0)
            {
                query = query.Where(x => x.DateCreated.Date.Month <= toMonth && x.DateCreated.Year <= toYear);
            }
            var totalRow = await query.CountAsync();

            query = query.OrderByDescending(x => x.DateCreated)
               .Skip((pageIndex - 1) * pageSize)
               .Take(pageSize);

            return new PageResult<TransactionDTO>
            {
                Results = await _mapper.ProjectTo<TransactionDTO>(query).ToListAsync(),
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };

        }

    }
}
