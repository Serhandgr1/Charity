using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface ISettingService
	{
		Task<List<SettingDto>> GetSettingList();
		Task<SettingDto> GetSettingById(int id);
		Task CreateSetting(SettingDto settingDto);
		Task UpdateSetting(SettingDto settingDto);
		Task DeleteSetting(int id);
	}
}
