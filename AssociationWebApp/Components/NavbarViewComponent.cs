using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace AssociationWebApp.Components
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;

        public NavbarViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var settings = await _settingService.GetSettingList();
            return View(settings.FirstOrDefault());
        }

    }

}
