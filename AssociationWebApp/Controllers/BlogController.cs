using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace AssociationWebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogController(IBlogService blogService, IBlogCategoryService blogCategoryService)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetBlogList();
            return View(blogs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var blog = await _blogService.GetBlogById(id);
            if (blog == null)
            {
                return NotFound("Blog not found.");
            }

            var category = await _blogCategoryService.GetBlogCategoryById(blog.BlogCategoriesId);
            if (category == null)
            {
                return NotFound("Blog category not found.");
            }

            blog.BlogCategory = category.Title;

            return View(blog);
        }
    }
}
