using Entities.Model;
using Entities.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Services.Contracts;

namespace AssociationWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly IStaffAccountService _staffAccountService;
        private readonly ILogger<StaffController> _logger;
        private readonly IDistributedCache _distributedCache;
        FileManagerAsync FileManager = new FileManagerAsync();
        public StaffController(IStaffService staffService, IStaffAccountService staffAccountService, ILogger<StaffController> logger, IDistributedCache distributedCache)
        {
            _staffService = staffService;
            _logger = logger;
            _distributedCache = distributedCache;
            _staffAccountService = staffAccountService;
        }
        [HttpGet]
        public IActionResult Show()
        {
            var staffList = _staffService.GetStaffList().Result.ToList();
            return View(staffList);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> AddStaffAccount([FromForm] StaffAccount staffAccount)
        //{
        //    var blogImageList = _blogImageService.GetBlogImageList().Result.Where(w => w.BlogId == blogImageDto.BlogId).ToList();
        //    var blogId = blogImageDto.BlogId;
        //    try
        //    {
        //        if (blogImageDto.FormFile is not null)
        //        {
        //            string name = await FileManager.PostFileAsync(blogImageDto.FormFile);
        //            blogImageDto.Image = name.Replace("wwwroot/", "");
        //            blogImageDto.FormFile = null;
        //            await _blogImageService.CreateBlogImage(blogImageDto);
        //            return RedirectToAction("ShowImages", "Blog", new { blogId = blogId });

        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return View("Error");
        //    }
        //    return RedirectToAction("ShowAccount", "Staff", new { blogId = blogId });
        //}
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] StaffDto staffDto)
        {
            if (staffDto.FormFile is not null)
            {
                try
                {
                    Localizer localizer = new Localizer(_distributedCache);
                    string name = await FileManager.PostFileAsync(staffDto.FormFile);
                    staffDto.Image = name.Replace("wwwroot/", "");
                    staffDto.FormFile = null;
                    await _staffService.CreateStaff(staffDto);
                    localizer.EnSetLanguage(staffDto.Content, staffDto.Content);
                    localizer.KrSetLanguage(staffDto.Content, staffDto.ContentKr);
                    localizer.TrSetLanguage(staffDto.Content, staffDto.ContentTr);
                    return RedirectToAction("Show", "Staff");
                }
                catch (Exception)
                {

                    return View("Error");
                }
            }
            return RedirectToAction("Show", "Staff");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {          
            var staff = _staffService.GetStaffById(id).Result;
            return View("Update", staff);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync([FromForm] StaffDto staffDto)
        {
            try
            {
                Localizer localizer = new Localizer(_distributedCache);
                var data = _staffService.GetStaffById(staffDto.Id).Result;
                if (staffDto.FormFile is not null)
                {
                    await FileManager.DeleteFileAsync(data.Image);
                    string name = await FileManager.PostFileAsync(staffDto.FormFile);
                    staffDto.Image = name.Replace("wwwroot/", "");
                    staffDto.FormFile = null;
                    await _staffService.UpdateStaff(staffDto);                  
                    localizer.UpdateLangue(staffDto.Content, staffDto.ContentKr, staffDto.Content, staffDto.ContentTr);
                }


                return RedirectToAction("Show", "Staff");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }


        }

        //public async Task<IActionResult> DeleteBlogImage(int id)
        //{
        //    var blog = _blogImageService.GetBlogImageById(id).Result;

        //    if (blog is not null)
        //    {
        //        await FileManager.DeleteFileAsync(blog.Image);
        //        await _blogImageService.DeleteBlogImage(id);

        //    }
        //    var blogId = blog.BlogId;
        //    return RedirectToAction("ShowImages", "Blog", new { blogId = blogId });
        //}
        public async Task<IActionResult> Delete(int id)
        {
            var staff = _staffService.GetStaffById(id).Result;

            if (staff is not null)
            {
                Localizer localizer = new Localizer(_distributedCache);
                await FileManager.DeleteFileAsync(staff.Image);
                await _staffService.DeleteStaff(id);               
                localizer.DeleteLanguage(staff.Content);

            }
            return RedirectToAction("Show", "Staff");
        }

        //public IActionResult ShowImages(int blogId)
        //{
        //    var blogImageList = _blogImageService.GetBlogImageList().Result.Where(w => w.BlogId == blogId).ToList();
        //    return View("ShowImages", Tuple.Create(blogImageList, blogId));

        //}
    }
}
