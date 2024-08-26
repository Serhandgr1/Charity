using Entities.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.EFCore;

namespace AssociationWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _manager;

        public SettingController(ISettingService settingService)
        {
            _manager = settingService;
        }
        public async Task<IActionResult> Show()
        {
            var settingList = await _manager.GetSettingList();
            return View(settingList);
        }
		public async Task<IActionResult> Details(int id)
		{
			var setting = await _manager.GetSettingById(id);

			if (setting == null)
			{
				return NotFound("Contact not found.");
			}

			return View(setting);
		}
		public async Task<IActionResult> Edit(int id)
        {
            var model = await _manager.GetSettingById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SettingDto settingDto)
        {
            if (ModelState.IsValid)
            {
                await _manager.UpdateSetting(settingDto);
                return RedirectToAction("Show");
            }

            return View(settingDto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SettingDto settingDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _manager.CreateSetting(settingDto);
                    return RedirectToAction(nameof(Show));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the setting.");
                }
            }
            return View(settingDto);
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var settingDto = await _manager.GetSettingById(id);
                if (settingDto == null)
                {
                    return NotFound();
                }
                return View(settingDto);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _manager.DeleteSetting(id);
                return RedirectToAction(nameof(Show));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
