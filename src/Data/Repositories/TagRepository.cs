using AutoMapper;
using Core.Domain.Content;
using Core.Models.Content;
using Core.Repositories;
using Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class TagRepository : RepositoryBase<Tag, Guid>, ITagRepository
    {
        private readonly IMapper _mapper;
        public TagRepository(IDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<TagDTO> GetBySlug(string slug)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Slug == slug);
            if (tag == null) return null;
            return _mapper.Map<TagDTO>(tag);
        }
    }
}
