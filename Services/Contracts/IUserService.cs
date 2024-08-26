using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IUserService
	{
		Task<List<UserDto>> GetUserList();
		Task<UserDto> GetUserById(int id);
		Task CreateUser(UserDto userDto);
		Task UpdateUser(UserDto userDto);
		Task DeleteUser(int id);
	}
}
