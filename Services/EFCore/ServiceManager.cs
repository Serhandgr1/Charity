using AutoMapper;
using Entities.Model;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFCore
{
	public class ServiceManager : IServiceManager
	{
		private readonly Lazy<IBlogCategoryService> _blogCategoryService;
		private readonly Lazy<IBlogService> _blogService;
		private readonly Lazy<IContactService> _contactService;
		private readonly Lazy<IMemberShipService> _memberShipService;
		private readonly Lazy<IBlogImageService> _blogImageService;
		private readonly Lazy<IStaffService> _staffService;
		private readonly Lazy<IStaffAccountService> _staffAccountService;
		private readonly Lazy<ISettingService> _settingService;
		private readonly Lazy<ISliderService> _sliderService;
		private readonly Lazy<IUserService> _userService;
		private readonly IConfiguration _configuration;
		private readonly Lazy<IUserService> _user;
		private readonly IUserService _userManager;

		public ServiceManager(IRepositoryManager repository, IMapper mapper, Microsoft.AspNetCore.Identity.UserManager<User> userManager, IConfiguration configuration)
		{
			// Servislerin tembel yükleme nesnelerinin oluşturulması
			_configuration = configuration;			
			_user = new Lazy<IUserService>(() => new UserService(repository, mapper));
			_blogService = new Lazy<IBlogService>(() => new BlogService(repository, mapper));
			_blogCategoryService = new Lazy<IBlogCategoryService>(() => new BlogCategoryService(repository, mapper));
			_contactService = new Lazy<IContactService>(() => new ContactService(repository, mapper));
			_memberShipService = new Lazy<IMemberShipService>(() => new MemberShipService(repository, mapper));
			_blogImageService = new Lazy<IBlogImageService>(() => new BlogImageService(repository, mapper));
			_staffService = new Lazy<IStaffService>(() => new StaffService(repository, mapper));
			_staffAccountService = new Lazy<IStaffAccountService>(() => new StaffAccountService(repository, mapper));
			_settingService = new Lazy<ISettingService>(() => new SettingService(repository, mapper));
			_sliderService = new Lazy<ISliderService>(() => new SliderService(repository, mapper));
			_userService = new Lazy<IUserService>(() => new UserService(repository, mapper));

		}
		public IBlogCategoryService blogCategoryService => _blogCategoryService.Value;

		public IBlogService blogService => _blogService.Value;

		public IContactService contactService => _contactService.Value;

		public IMemberShipService membershipService => _memberShipService.Value;

		public IBlogImageService blogImageService => _blogImageService.Value;

		public IStaffService staffService => _staffService.Value;
		public IStaffAccountService staffAccountService => _staffAccountService.Value;

		public ISettingService settingService => _settingService.Value;

		public ISliderService sliderService => _sliderService.Value;

		public IUserService userService => _userService.Value;
	}
}
