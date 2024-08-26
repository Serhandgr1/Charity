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
	public class SettingService : ISettingService
	{
		private readonly IRepositoryManager _repository;
		private readonly IMapper _mapper;
		public SettingService(IRepositoryManager repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateSetting(SettingDto settingDto)
		{
			var setting = _mapper.Map<Setting>(settingDto);
			await _repository.Setting.Create(setting);
			_repository.Save();
		}

		public async Task DeleteSetting(int id)
		{
			var setting = _repository.Setting.GetById(id).Result;
			if (setting != null)
			{
				await _repository.Setting.Delete(setting);
				_repository.Save();
			}
		}

		public async Task<SettingDto> GetSettingById(int id)
		{
			var setting = await _repository.Setting.GetById(id);
			return _mapper.Map<SettingDto>(setting);
		}

		public async Task<List<SettingDto>> GetSettingList()
		{
			var settings = await _repository.Setting.Read(false);
			return _mapper.Map<List<SettingDto>>(settings);
		}

		public async Task UpdateSetting(SettingDto settingDto)
		{
			var setting = _mapper.Map<Setting>(settingDto);
			await _repository.Setting.Update(setting);
			_repository.Save();
		}


	}
}
