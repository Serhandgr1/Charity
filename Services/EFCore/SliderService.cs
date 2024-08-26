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
	public class SliderService : ISliderService
	{
		private readonly IRepositoryManager _repository;
		private readonly IMapper _mapper;
		public SliderService(IRepositoryManager repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateSlider(SliderDto sliderDto)
		{
			var slider = _mapper.Map<Slider>(sliderDto);
			await _repository.Slider.Create(slider);
			_repository.Save();
		}

		public async Task DeleteSlider(int id)
		{
			var slider = _repository.Slider.GetById(id).Result;
			if (slider != null)
			{
				await _repository.Slider.Delete(slider);
				_repository.Save();
			}
		}

		public async Task<SliderDto> GetSliderById(int id)
		{
			var slider = await _repository.Slider.GetById(id);
			return _mapper.Map<SliderDto>(slider);
		}

		public async Task<List<SliderDto>> GetSliderList()
		{
			var sliders = await _repository.Slider.Read(false);
			return _mapper.Map<List<SliderDto>>(sliders);
		}

		public async Task UpdateSlider(SliderDto sliderDto)
		{
			var slider = _mapper.Map<Slider>(sliderDto);
			await _repository.Slider.Update(slider);
			_repository.Save();
		}


	}
}
