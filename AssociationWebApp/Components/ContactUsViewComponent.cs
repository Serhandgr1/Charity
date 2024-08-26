using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace AssociationWebApp.Components
{
    public class ContactUsViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;

        public ContactUsViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = await _settingService.GetSettingById(1);

            return View(setting);
        }
    }
}
