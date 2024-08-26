using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Threading.Tasks;

namespace AssociationWebApp.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISliderService _sliderService;

        public SliderViewComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _sliderService.GetSliderList();
            return View(sliders);
        }
    }
}
