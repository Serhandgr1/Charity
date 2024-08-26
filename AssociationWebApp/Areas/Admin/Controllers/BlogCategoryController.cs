using Entities.Model;
using Entities.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Services.Contracts;


namespace AssociationWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogCategoryController : Controller
    {
        private readonly IBlogCategoryService _blogCategoryService;

        private readonly ILogger<BlogCategoryController> _logger;
        private readonly IDistributedCache _distributedCache;
        public BlogCategoryController(IBlogCategoryService blogCategoryService, ILogger<BlogCategoryController> logger, IDistributedCache distributedCache)
        {
            _blogCategoryService = blogCategoryService;
            _logger = logger;
            _distributedCache = distributedCache;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _blogCategoryService.GetBlogCategoryById(id).Result;
            return View(category);
        }
        [HttpGet]
        public IActionResult Show()
        {
            var categories = _blogCategoryService.GetBlogCategoryList().Result.ToList();
            return View(categories);
        }
        [HttpPost]
        public IActionResult Add([FromForm] BlogCategoryDto blogCategoryDto)
        {
            try
            {
                Localizer localizer = new Localizer(_distributedCache);
                _blogCategoryService.CreateBlogCategory(blogCategoryDto);
                localizer.KrSetLanguage(blogCategoryDto.Title, blogCategoryDto.TitleKr);
                localizer.EnSetLanguage(blogCategoryDto.Title, blogCategoryDto.Title);
                localizer.TrSetLanguage(blogCategoryDto.Title, blogCategoryDto.TitleTr);
                return RedirectToAction("Show", "BlogCategory");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }



        }
        public List<BlogCategoryDto> GetAll()
        {
            var data = _blogCategoryService.GetBlogCategoryList().Result;
            return data;
        }
        public BlogCategoryDto GetById(int id)
        {
            var data = _blogCategoryService.GetBlogCategoryById(id).Result;
            return data;
        }
        [HttpPost]
        public IActionResult Update([FromForm] BlogCategoryDto blogCategoryDto)
        {
            try
            {
                Localizer localizer = new Localizer(_distributedCache);
                localizer.UpdateLangue(blogCategoryDto.Title, blogCategoryDto.TitleKr, blogCategoryDto.Title, blogCategoryDto.TitleTr);
                _blogCategoryService.UpdateBlogCategory(blogCategoryDto);
                return RedirectToAction("Show", "BlogCategory");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }
      
        public IActionResult Delete(int id)
        {
            try
            {
                var data = _blogCategoryService.GetBlogCategoryById(id).Result;
                if (data != null)
                {
                    _blogCategoryService.DeleteBlogCategory(id);
                    Localizer localizer = new Localizer(_distributedCache);
                    localizer.DeleteLanguage(data.Title);
                }
                else return BadRequest();
                return RedirectToAction("Show", "BlogCategory");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
