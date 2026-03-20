using Core.Models;
using Core.Models.Content;

namespace WebApp.Models
{
    public class PostListByCategoryViewModel
    {
        public PostCategoryDTO Category { get; set; }
        public PageResult<PostInListDTO>? Posts { get; set; }
    }
}
