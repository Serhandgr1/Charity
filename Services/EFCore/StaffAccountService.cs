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
	public class StaffAccountService : IStaffAccountService
	{
		private readonly IRepositoryManager _repository;
		private readonly IMapper _mapper;
		public StaffAccountService(IRepositoryManager repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateStaffAccount(StaffAccountDto staffAccountDto)
		{
			var staffAccount = _mapper.Map<StaffAccount>(staffAccountDto);
			await _repository.StaffAccount.Create(staffAccount);
			_repository.Save();
		}

		public async Task DeleteStaffAccount(int id)
		{
			var staffAccount = _repository.StaffAccount.GetById(id).Result;
			if (staffAccount != null)
			{
				await _repository.StaffAccount.Delete(staffAccount);
				_repository.Save();
			}
		}

		public async Task<StaffAccountDto> GetStaffAccountById(int id)
		{
			var staffAccount = await _repository.StaffAccount.GetById(id);
			return _mapper.Map<StaffAccountDto>(staffAccount);
		}

		public async Task<List<StaffAccountDto>> GetStaffAccountList()
		{
			var staffAccounts = await _repository.StaffAccount.Read(false);
			return _mapper.Map<List<StaffAccountDto>>(staffAccounts);
		}

		public async Task UpdateStaffAccount(StaffAccountDto staffAccountDto)
		{
			var staffAccount = _mapper.Map<StaffAccount>(staffAccountDto);
			await _repository.StaffAccount.Update(staffAccount);
			_repository.Save();
		}


	}
}
