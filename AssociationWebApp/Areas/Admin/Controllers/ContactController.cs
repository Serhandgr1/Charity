using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Threading.Tasks;

namespace AssociationWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Show()
        {
            var contacts = await _contactService.GetContactList();
            return View(contacts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var contact = await _contactService.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }
    }
}
