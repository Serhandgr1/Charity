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
	public class UserService : IUserService
	{
		private readonly IRepositoryManager _repository;
		private readonly IMapper _mapper;
		public UserService(IRepositoryManager repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateUser(UserDto userDto)
		{
			var user = _mapper.Map<User>(userDto);
			await _repository.User.Create(user);
			_repository.Save();
		}

		public async Task DeleteUser(int id)
		{
			var user = _repository.User.GetById(id).Result;
			if (user != null)
			{
				await _repository.User.Delete(user);
				_repository.Save();
			}
		}

		public async Task<UserDto> GetUserById(int id)
		{
			var user = await _repository.User.GetById(id);
			return _mapper.Map<UserDto>(user);
		}

		public async Task<List<UserDto>> GetUserList()
		{
			var users = await _repository.User.Read(false);
			return _mapper.Map<List<UserDto>>(users);
		}

		public async Task UpdateUser(UserDto userDto)
		{
			var user = _mapper.Map<User>(userDto);
			await _repository.User.Update(user);
			_repository.Save();
		}


	}
}
