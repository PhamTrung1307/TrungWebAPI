using AutoMapper;
using Core.Domain.Identity;
using Core.Repositories;
using Core.SeedWorks;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Data.SeedWorks
{
    public class  UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;
        public UnitOfWork(IDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            Posts = new PostRepository(context, mapper, userManager);
            PostCategories = new PostCategoryRepository(context, mapper);
            Series = new SeriesRepository(context, mapper);
            Transactions = new TransactionRepository(_context, mapper);
            Tags = new TagRepository(context, mapper); 
            Users = new UserRepository(context);
        }

        public IPostRepository Posts { get; private set; }

        public IPostCategoryRepository PostCategories { get; private set; }

        public ISeriesRepository Series { get; private set; }

        public ITransactionRepository Transactions { get; private set; }
        public IUserRepository Users { get; private set; }
        public ITagRepository Tags { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
