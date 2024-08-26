using Entities.Model;
using Entities.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Services.Contracts;
using System;

namespace AssociationWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogImageService _blogImageService;
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly ILogger<BlogController> _logger;
        private readonly IDistributedCache _distributedCache;
        FileManagerAsync FileManager = new FileManagerAsync();
        public BlogController(IBlogService blogService, IBlogCategoryService blogCategoryService, ILogger<BlogController> logger, IDistributedCache distributedCache, IBlogImageService blogImageService)
        {
            _blogService = blogService;
            _logger = logger;
            _distributedCache = distributedCache;
            _blogCategoryService = blogCategoryService;
            _blogImageService = blogImageService;
        }
        [HttpGet]
        public IActionResult Show()
        {
            var blogList = _blogService.GetBlogList().Result.ToList();
            foreach (var blog in blogList)
            {
                blog.BlogCategory = _blogCategoryService.GetBlogCategoryById(blog.BlogCategoriesId).Result.Title;
            }
            return View(blogList);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var blogCategoryList = _blogCategoryService.GetBlogCategoryList().Result.ToList();
            BlogDto blog = new BlogDto();
            return View("Add", Tuple.Create(blogCategoryList, blog));
        }
        [HttpPost]
        public async Task<IActionResult> AddBlogImage([FromForm] BlogImageDto blogImageDto)
        {
            var blogImageList = _blogImageService.GetBlogImageList().Result.Where(w => w.BlogId == blogImageDto.BlogId).ToList();
            var blogId=blogImageDto.BlogId;
            try
            {
                if (blogImageDto.FormFile is not null)
                {
                    string name = await FileManager.PostFileAsync(blogImageDto.FormFile);
                    blogImageDto.Image = name.Replace("wwwroot/", "");
                    blogImageDto.FormFile = null;
                    await _blogImageService.CreateBlogImage(blogImageDto);
                    return RedirectToAction("ShowImages", "Blog", new { blogId = blogId });
             
                }
            }
            catch (Exception)
            {

                return View("Error");
            }
            return RedirectToAction("ShowImages", "Blog", new { blogId = blogId });
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] BlogDto blogDto)
        {
            if (blogDto.FormFile is not null)
            {
                try
                {
                    Localizer localizer = new Localizer(_distributedCache);
                    string name = await FileManager.PostFileAsync(blogDto.FormFile);
                    blogDto.Image = name.Replace("wwwroot/", "");
                    blogDto.FormFile = null;
                    await _blogService.CreateBlog(blogDto);
                    localizer.KrSetLanguage(blogDto.Title, blogDto.TitleKr);
                    localizer.EnSetLanguage(blogDto.Title, blogDto.Title);
                    localizer.TrSetLanguage(blogDto.Title, blogDto.TitleTr);
                    localizer.KrSetLanguage(blogDto.Content, blogDto.ContentKr);
                    localizer.EnSetLanguage(blogDto.Content, blogDto.Content);
                    localizer.TrSetLanguage(blogDto.Content, blogDto.ContentTr);
                    return RedirectToAction("Show", "Blog");
                }
                catch (Exception)
                {

                    return View("Error");
                }
            }
            return RedirectToAction("Show", "blog");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var blogCategoryList = _blogCategoryService.GetBlogCategoryList().Result.ToList();
            var blog = _blogService.GetBlogById(id).Result;
            return View("Update", Tuple.Create(blogCategoryList, blog));

        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync([FromForm] BlogDto blogDto)
        {
            try
            {
                Localizer localizer = new Localizer(_distributedCache);
                var data = _blogService.GetBlogById(blogDto.Id).Result;
                if (blogDto.FormFile is not null)
                {
                    await FileManager.DeleteFileAsync(data.Image);
                    string name = await FileManager.PostFileAsync(blogDto.FormFile);
                    blogDto.Image = name.Replace("wwwroot/", "");
                    blogDto.FormFile = null;
                    await _blogService.UpdateBlog(blogDto);
                    localizer.UpdateLangue(blogDto.Title, blogDto.TitleKr, blogDto.Title, blogDto.TitleTr);
                    localizer.UpdateLangue(blogDto.Content, blogDto.ContentKr, blogDto.Content, blogDto.ContentTr);
                }


                return RedirectToAction("Show", "Blog");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }


        }

        public async Task<IActionResult> DeleteBlogImage(int id)
        {
            var blog = _blogImageService.GetBlogImageById(id).Result;

            if (blog is not null)
            {
                await FileManager.DeleteFileAsync(blog.Image);
                await _blogImageService.DeleteBlogImage(id);
                
            }
            var blogId = blog.BlogId;
            return RedirectToAction("ShowImages", "Blog", new { blogId = blogId });
        }
        public async Task<IActionResult> Delete(int id)
        {
            var blog = _blogService.GetBlogById(id).Result;

            if (blog is not null)
            {
                Localizer localizer = new Localizer(_distributedCache);
                await FileManager.DeleteFileAsync(blog.Image);
                await _blogService.DeleteBlog(id);
                localizer.DeleteLanguage(blog.Title);
                localizer.DeleteLanguage(blog.Content);

            }
            return RedirectToAction("Show", "Blog");
        }

        public IActionResult ShowImages(int blogId)
        {
            var blogImageList = _blogImageService.GetBlogImageList().Result.Where(w => w.BlogId == blogId).ToList();
            return View("ShowImages", Tuple.Create(blogImageList, blogId));

        }

    }
}
