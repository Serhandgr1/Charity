using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IServiceManager
	{
		IBlogCategoryService blogCategoryService { get; }
		IBlogService blogService { get; }
		IContactService contactService { get; }
		IMemberShipService membershipService { get; }
		IBlogImageService blogImageService { get; }
		IStaffService staffService { get; }
		IStaffAccountService staffAccountService { get; }
		ISettingService settingService { get; }
		ISliderService sliderService { get; }
		IUserService userService { get; }
	}
}
