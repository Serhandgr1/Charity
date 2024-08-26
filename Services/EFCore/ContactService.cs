using AutoMapper;
using Entities.Model;
using Entities.ModelDto;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFCore
{
	public class ContactService : IContactService
	{
		private readonly IRepositoryManager _repository;
		private readonly IMapper _mapper;
		public ContactService(IRepositoryManager repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateContact(ContactDto contactDto)
		{
			var contact = _mapper.Map<Contact>(contactDto);
			await _repository.Contact.Create(contact);
			_repository.Save();
		}

		public async Task DeleteContact(int id)
		{
			var contact = _repository.Contact.GetById(id).Result;
			if (contact != null)
			{
				await _repository.Contact.Delete(contact);
				_repository.Save();
			}
		}

		public async Task<ContactDto> GetContactById(int id)
		{
			var contact = await _repository.Contact.GetById(id);
			return _mapper.Map<ContactDto>(contact);
		}

		public async Task<List<ContactDto>> GetContactList()
		{
			var contacts = await _repository.Contact.Read(false);
			return _mapper.Map<List<ContactDto>>(contacts);
		}

		public async Task UpdateContact(ContactDto contactDto)
		{
			var contact = _mapper.Map<Contact>(contactDto);
			await _repository.Contact.Update(contact);
			_repository.Save();
		}


	}
}
