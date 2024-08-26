using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IStaffService
	{
		Task<List<StaffDto>> GetStaffList();
		Task<StaffDto> GetStaffById(int id);
		Task CreateStaff(StaffDto staffDto);
		Task UpdateStaff(StaffDto staffDto);
		Task DeleteStaff(int id);
	}
}
