using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
	public interface IRepositoryManager
	{
		IRepositorySetting Setting { get; }
		IRepositoryBlogCategory BlogCategory { get; }
		IRepositoryBlog Blog { get; }
		IRepositoryContact Contact { get; }
		IRepositoryBlogImage BlogImage { get; }
		IRepositoryUser User { get; }
		IRepositorySlider Slider { get; }
		IRepositoryMemberShip MemberShip { get; }
		IRepositoryStaff Staff { get; }
		IRepositoryStaffAccount StaffAccount { get; }
		IRepositoryAnswer Answer { get; }

		void Save(); // unit of work kullanımı 
	}
}
