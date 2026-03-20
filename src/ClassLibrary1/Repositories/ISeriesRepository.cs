using Core.Domain.Content;
using Core.Models;
using Core.Models.Content;
using Core.SeedWorks;

namespace Core.Repositories
{
    public interface ISeriesRepository : IRepository<Series, Guid>
    {
        Task<PageResult<SeriesInListDTO>> GetAllPaging(string? keyword, int pageIndex = 1, int pageSize = 10);
        Task AddPostToSeries(Guid seriesId, Guid postId, int sortOrder);
        Task RemovePostToSeries(Guid seriesId, Guid postId);
        Task<List<PostInListDTO>> GetAllPostsInSeries(Guid seriesId);
        Task<PageResult<PostInListDTO>> GetAllPostsInSeries(string slug, int pageIndex = 1, int pageSize = 10);
        Task<SeriesDTO> GetBySlug(string slug);

        Task<bool> IsPostInSeries(Guid seriesId, Guid postId);
        Task<bool> HasPost(Guid seriesId);
    }
}
