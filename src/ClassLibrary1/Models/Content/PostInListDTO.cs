using AutoMapper;
using Core.Domain.Content;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Content
{
    public class PostInListDTO
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Slug { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public string? Thumbnail { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }
       // public required string CategorySlug { set; get; }

        //public required string CategoryName { set; get; }
        //public string? AuthorUserName { set; get; }
        //public string? AuthorName { set; get; }

        //public PostStatus Status { set; get; }
        //public bool IsPaid { get; set; }
        //public double RoyaltyAmount { get; set; }
        //public DateTime? PaidDate { get; set; }

        public class AutomapperProfile : Profile
        {
            public AutomapperProfile()
            {
                CreateMap<Post, PostInListDTO>();           
            }
        }
    }
}
