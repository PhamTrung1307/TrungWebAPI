using Core.Domain.Content;
using Core.Models;
using Core.Models.Content;
using Core.SeedWorks;

namespace Core.Repositories
{
    public interface IPostRepository : IRepository<Post, Guid>
    {
        Task<PageResult<PostInListDTO>> GetAllPaging(string? keyword, Guid currentUserId, Guid? categoryId, int pageIndex = 1, int pageSize = 10);
        Task<bool> IsSlugAlreadyExisted(string slug, Guid? currentId = null);
        Task<List<SeriesInListDTO>> GetAllSeries(Guid postId);
        Task Approve(Guid id, Guid currentUserId);
        Task SenDTOApprove(Guid id, Guid currentUserId);
        Task ReturnBack(Guid id, Guid currentUserId, string note);
        Task<string> GetReturnReason(Guid id);
        Task<bool> HasPublishInLast(Guid id);
        Task<List<PostActivityLogDTO>> GetActivityLogs(Guid id);
        Task<List<Post>> GetListUnpaidPublishPosts(Guid userId);
        Task<List<PostInListDTO>> GetLatestPublishPost(int top);

        Task<PostDTO> GetBySlug(string slug);
        Task<PageResult<PostInListDTO>> GetPostByCategoryPaging(string? categorySlug, int pageIndex = 1, int pageSize = 10);
        Task<List<string>> GetAllTags();
        Task AddTagToPost(Guid postId, Guid tagId);

        Task<List<TagDTO>> GetTagObjectsByPostId(Guid postId);
        Task<PageResult<PostInListDTO>> GetPostByTagPaging(string tagSlug, int pageIndex = 1, int pageSize = 10);
        Task<List<string>> GetTagsByPostId(Guid postId);
        Task<PageResult<PostInListDTO>> GetPostByUserPaging(string keyword, Guid userId, int pageIndex = 1, int pageSize = 10);






    }
}
