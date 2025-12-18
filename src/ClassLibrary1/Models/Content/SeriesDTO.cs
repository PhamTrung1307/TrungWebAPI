using AutoMapper;
using Core.Domain.Content;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Content
{
    public class SeriesDTO : SeriesInListDTO
    {
        [MaxLength(250)]
        public string? SeoDescription { get; set; }

        [MaxLength(250)]
        public string? Thumbnail { set; get; }

        public string? Content { get; set; }

        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Series, SeriesDTO>();
            }
        }
    }
}
