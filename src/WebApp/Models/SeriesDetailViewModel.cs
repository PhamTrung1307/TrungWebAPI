using Core.Models;
using Core.Models.Content;

namespace TeduBlog.WebApp.Models
{
    public class SeriesDetailViewModel
    {
        public SeriesDTO Series { get; set; }

        public PageResult<PostInListDTO> Posts { get; set; }
    }
}
