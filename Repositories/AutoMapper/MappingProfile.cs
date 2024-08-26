using AutoMapper;
using Entities.Model;
using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.AutoMapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<BlogDto, Blog>().ReverseMap();
			CreateMap<BlogCategoryDto, BlogCategory>().ReverseMap();
			CreateMap<BlogImageDto, BlogImage>().ReverseMap();
			CreateMap<ContactDto, Contact>().ReverseMap();
			CreateMap<MemberShipDto, MemberShip>().ReverseMap();
			CreateMap<SettingDto, Setting>().ReverseMap();
			CreateMap<SliderDto, Slider>().ReverseMap();
			CreateMap<StaffDto, Staff>().ReverseMap();
			CreateMap<StaffAccountDto, StaffAccount>().ReverseMap();
			CreateMap<UserDto, User>().ReverseMap();
			CreateMap<QuestionDto, Question>().ReverseMap();
			CreateMap<AnswerDto, Answer>().ReverseMap();
			CreateMap<SurveyDto, Survey>().ReverseMap();
		}
	}
}
