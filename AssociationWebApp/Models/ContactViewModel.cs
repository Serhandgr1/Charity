using Entities.ModelDto;

namespace AssociationWebApp.Models
{
	public class ContactViewModel
	{
		public ContactDto NewContact { get; set; } = new ContactDto();
		public List<ContactDto> Comments { get; set; } = new List<ContactDto>();
	}

}
