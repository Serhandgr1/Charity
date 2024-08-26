using AssociationWebApp.Models;
using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace AssociationWebApp.Components
{
	public class CommentViewComponent : ViewComponent
	{
		private readonly IContactService _manager;

		public CommentViewComponent(IContactService manager)
		{
			_manager = manager;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var comments = await _manager.GetContactList();
			var viewModel = new ContactViewModel
			{
				NewContact = new ContactDto(),
				Comments = comments
			};
			return View(viewModel);
		}
	}

}
