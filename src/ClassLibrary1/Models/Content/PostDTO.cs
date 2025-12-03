using AutoMapper;
using Core.Domain.Content;

namespace Core.Models.Content
{
    public class PostDTO : PostInListDTO
    {
        public Guid CategoryId { get; set; }

        public string? Content { get; set; }

        public Guid AuthorUserId { get; set; }

        public string? Source { get; set; }

        public string? Tags { get; set; }

        public string? SeoDescription { get; set; }

        public DateTime? DateModified { get; set; }

        public class AutomapperProfile : Profile
        {
            public AutomapperProfile()
            {
                CreateMap<Post, PostDTO>();
            }
        }
    }
}
