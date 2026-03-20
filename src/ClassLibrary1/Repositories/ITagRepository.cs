using Core.Domain.Content;
using Core.Models.Content;
using Core.SeedWorks;

namespace Core.Repositories
{
    public interface ITagRepository : IRepository<Tag, Guid>
    {
        Task<TagDTO> GetBySlug(string slug);
    }
}
