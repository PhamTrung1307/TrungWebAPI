using Core.Models.Content;
using Core.Models;

namespace WebApp.Models
{
    public class PostListByTagViewModel
    {
        public TagDTO Tag { get; set; }
        public PageResult<PostInListDTO> Posts { get; set; }
    }
}
