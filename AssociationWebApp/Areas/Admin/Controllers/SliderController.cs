using Entities.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Services.Contracts;
using Services.EFCore;

namespace AssociationWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ILogger<SliderController> _logger;
        private readonly IDistributedCache _distributedCache;
        FileManagerAsync FileManager = new FileManagerAsync();
        public SliderController(ISliderService sliderService, ILogger<SliderController> logger, IDistributedCache distributedCache)
        {
            _sliderService = sliderService;
            _logger = logger;
            _distributedCache = distributedCache;

        }
       
        public IActionResult Show()
        {
            var sliders=_sliderService.GetSliderList().Result;
            return View(sliders);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var slider= _sliderService.GetSliderById(id).Result;
            return View(slider);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] SliderDto sliderDto)
        {
            if (sliderDto.FormFile != null)
            {
                var slider = await _sliderService.GetSliderById(sliderDto.Id);

                if (slider != null)
                {
                    try
                    {
                        Localizer localizer = new Localizer(_distributedCache);
                        await FileManager.DeleteFileAsync(slider.Image);

                        string name = await FileManager.PostFileAsync(sliderDto.FormFile);
                        sliderDto.Image = name.Replace("wwwroot/", "");
                        sliderDto.FormFile = null;

                        await _sliderService.UpdateSlider(sliderDto);

                        localizer.UpdateLangue(sliderDto.Title, sliderDto.TitleKr, sliderDto.Title, sliderDto.TitleTr);
                        localizer.UpdateLangue(sliderDto.Content, sliderDto.ContentKr, sliderDto.Content, sliderDto.ContentTr);

                        return RedirectToAction("Show", "Slider");
                    }
                    catch (Exception)
                    {
                        return View("Error");
                    }
                }
            }

            return RedirectToAction("Show", "Slider");
        }

        public async Task<IActionResult> Add([FromForm] SliderDto sliderDto)
        {
            if (sliderDto.FormFile is not null)
            {
                try
                {
                    Localizer localizer = new Localizer(_distributedCache);
                    string name = await FileManager.PostFileAsync(sliderDto.FormFile);
                    sliderDto.Image = name.Replace("wwwroot/", "");
                    sliderDto.FormFile = null;
                    await _sliderService.CreateSlider(sliderDto);
                    localizer.KrSetLanguage(sliderDto.Title, sliderDto.TitleKr);
                    localizer.EnSetLanguage(sliderDto.Title, sliderDto.Title);
                    localizer.TrSetLanguage(sliderDto.Title, sliderDto.TitleTr);
                    localizer.KrSetLanguage(sliderDto.Content, sliderDto.ContentKr);
                    localizer.EnSetLanguage(sliderDto.Content, sliderDto.Content);
                    localizer.TrSetLanguage(sliderDto.Content, sliderDto.ContentTr);
                    return RedirectToAction("Show", "Slider");
                }
                catch (Exception)
                {

                    return View("Error");
                }
            }
            return RedirectToAction("Show", "Slider");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var slider = _sliderService.GetSliderById(id).Result;
            if (slider is not null)
            {
                Localizer localizer = new Localizer(_distributedCache);
                await FileManager.DeleteFileAsync(slider.Image);
                await _sliderService.DeleteSlider(id);
                localizer.DeleteLanguage(slider.Title);
                localizer.DeleteLanguage(slider.Content);
            }
            return RedirectToAction("Show", "Slider");
        }
    }
}
