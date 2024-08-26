using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface ISliderService
	{
		Task<List<SliderDto>> GetSliderList();
		Task<SliderDto> GetSliderById(int id);
		Task CreateSlider(SliderDto sliderDto);
		Task UpdateSlider(SliderDto sliderDto);
		Task DeleteSlider(int id);
	}
}
