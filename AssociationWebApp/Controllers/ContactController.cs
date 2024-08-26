using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.EFCore;

namespace AssociationWebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _manager;

        public ContactController(IContactService manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] ContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                await _manager.CreateContact(contactDto);
                TempData["SuccessMessage"] = "Mesajınız Alınmıştır, Teşekkürler.";
                return RedirectToAction("Index");
            }
            return View(contactDto);
        }
    }
}
