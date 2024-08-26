using AssociationWebApp.Models;
using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.EFCore;

namespace AssociationWebApp.Controllers
{
	public class AboutUsController : Controller
	{
		private readonly IContactService _manager;

		public AboutUsController(IContactService manager)
		{
			_manager = manager;
		}

		public async Task<IActionResult> Index()
		{
			var comments = await _manager.GetContactList();
			var viewModel = new ContactViewModel
			{
				NewContact = new ContactDto(),
				Comments = comments
			};
			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Index(ContactViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				await _manager.CreateContact(viewModel.NewContact);
				TempData["message"] = "Mesajınız Alınmıştır, Teşekkürler.";
				return RedirectToAction("Index");
			}

			viewModel.Comments = await _manager.GetContactList();
			return View(viewModel);
		}


		public async Task<IActionResult> Details(int id)
		{
			var contact = await _manager.GetContactById(id);

			if (contact == null)
			{
				return NotFound("Contact not found.");
			}

			return View(contact);
		}
	}
}
