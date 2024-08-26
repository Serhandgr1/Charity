using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IStaffAccountService
	{
		Task<List<StaffAccountDto>> GetStaffAccountList();
		Task<StaffAccountDto> GetStaffAccountById(int id);
		Task CreateStaffAccount(StaffAccountDto staffAccountDto);
		Task UpdateStaffAccount(StaffAccountDto staffAccountDto);
		Task DeleteStaffAccount(int id);
	}
}
