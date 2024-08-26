using AssociationWebApp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AssociationWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISettingService _settingService;

        public HomeController(ILogger<HomeController> logger, ISettingService settingService)
        {
            _logger = logger;
            _settingService = settingService;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _settingService.GetSettingList();
            var setting = settings.FirstOrDefault();

            return View(setting);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
              CookieRequestCultureProvider.DefaultCookieName,
              CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
              new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
              );
            //return RedirectToAction("Index");
            return LocalRedirect(returnUrl);
        }
    }
}
