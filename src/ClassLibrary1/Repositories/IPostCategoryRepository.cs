using Core.Domain.Content;
using Core.Models;
using Core.Models.Content;
using Core.SeedWorks;

namespace Core.Repositories
{
    public interface IPostCategoryRepository : IRepository<PostCategory, Guid>
    {
        Task<PageResult<PostCategoryDTO>> GetAllPaging(string? keyword, int pageIndex = 1, int pageSize = 10);
        Task<bool> HasPost(Guid categoryId);
        Task<PostCategoryDTO> GetBySlug(string slug);
    }
}
