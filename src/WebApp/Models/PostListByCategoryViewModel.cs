using Core.Models.Content;
using Core.Models;

namespace WebApp.Models
{
    public class PostListByCategoryViewModel
    {
        public PostCategoryDTO Category { get; set; }
        public PageResult<PostInListDTO> Posts { get; set; }
    }
}
