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
	public class StaffService : IStaffService
	{
		private readonly IRepositoryManager _repository;
		private readonly IMapper _mapper;
		public StaffService(IRepositoryManager repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateStaff(StaffDto staffDto)
		{
			var staff = _mapper.Map<Staff>(staffDto);
			await _repository.Staff.Create(staff);
			_repository.Save();
		}

		public async Task DeleteStaff(int id)
		{
			var staff = _repository.Staff.GetById(id).Result;
			if (staff != null)
			{
				await _repository.Staff.Delete(staff);
				_repository.Save();
			}
		}

		public async Task<StaffDto> GetStaffById(int id)
		{
			var staff = await _repository.Staff.GetById(id);
			return _mapper.Map<StaffDto>(staff);
		}

		public async Task<List<StaffDto>> GetStaffList()
		{
			var staffs = await _repository.Staff.Read(false);
			return _mapper.Map<List<StaffDto>>(staffs);
		}

		public async Task UpdateStaff(StaffDto staffDto)
		{
			var staff = _mapper.Map<Staff>(staffDto);
			await _repository.Staff.Update(staff);
			_repository.Save();
		}


	}
}
