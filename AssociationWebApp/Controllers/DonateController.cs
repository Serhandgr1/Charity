using Microsoft.AspNetCore.Mvc;

namespace AssociationWebApp.Controllers
{
    public class DonateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
