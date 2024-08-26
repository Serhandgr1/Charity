using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace AssociationWebApp.Components
{
    public class BlogCategoriesViewComponent : ViewComponent
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoriesViewComponent(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _blogCategoryService.GetBlogCategoryList();
            return View(categories);
        }
    }

}
