using Core.Domain.Content;
using Core.Models;
using Core.Models.Content;
using Core.SeedWorks;

namespace Core.Repositories
{
    public interface IPostRepository : IRepository<Post, Guid>
    {
      Task<List<Post>> GetPopularPostsAsync(int count);
      Task<PageResult<PostInListDTO>> GetPagedPostsAsync(string? key,Guid? categoryId,int pageIndex=1, int pageSize=10);
    }
}
