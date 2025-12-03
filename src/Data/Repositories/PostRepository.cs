using AutoMapper;
using Core.Domain.Content;
using Core.Models;
using Core.Models.Content;
using Core.Repositories;
using Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PostRepository : RepositoryBase<Post, Guid>, IPostRepository
    {
        private readonly IMapper _mapper;
        public PostRepository(IDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public Task<List<Post>> GetPopularPostsAsync(int count)
        {
            return _context.Set<Post>().OrderByDescending(p => p.ViewCount).Take(count).ToListAsync();
        }

        public async Task<PageResult<PostInListDTO>> GetPagedPostsAsync(string key, Guid? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Posts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(key))
            {
                query = query.Where(p => p.Name.Contains(key) || p.Description!.Contains(key));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryID == categoryId);
            }

            var totalRow = await query.CountAsync();
            query = query.OrderByDescending(p => p.DateCreated)
                         .Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);
            
            return new PageResult<PostInListDTO>
            {
                Results = await _mapper.ProjectTo<PostInListDTO>(query).ToListAsync(),
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };
        }
    }
}
