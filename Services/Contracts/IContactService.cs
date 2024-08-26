using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IContactService
	{
		Task<List<ContactDto>> GetContactList();
		Task<ContactDto> GetContactById(int id);
		Task CreateContact(ContactDto contactDto);
		Task UpdateContact(ContactDto contactDto);
		Task DeleteContact(int id);
	}
}
