using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
	public class RepositoryManager : IRepositoryManager
	{
		private readonly RepositoryContext _context;
		private readonly Lazy<IRepositoryUser> _repositoryUser;
		private readonly Lazy<IRepositoryBlog> _repositoryBlog;
		private readonly Lazy<IRepositoryBlogCategory> _repositoryBlogCategory;
		private readonly Lazy<IRepositoryContact> _repositoryContact;
		private readonly Lazy<IRepositoryMemberShip> _repositoryMemberShip;
		private readonly Lazy<IRepositoryBlogImage> _repositoryBlogImage;
		private readonly Lazy<IRepositoryStaff> _repositoryStaff;
		private readonly Lazy<IRepositoryStaffAccount> _repositoryStaffAccount;
		private readonly Lazy<IRepositorySetting> _repositorySetting;
		private readonly Lazy<IRepositorySlider> _repositorySlider;
		private readonly Lazy<IRepositoryAnswer> _repositoryAnswer;



		public RepositoryManager(RepositoryContext context)
		{
			_context = context;
			_repositoryUser = new Lazy<IRepositoryUser>(() => new RepositoryUser(_context));
			_repositoryBlog = new Lazy<IRepositoryBlog>(() => new RepositoryBlog(_context));
			_repositoryBlogCategory = new Lazy<IRepositoryBlogCategory>(() => new RepositoryBlogCategory(_context));
			_repositoryContact = new Lazy<IRepositoryContact>(() => new RepositoryContact(_context));
			_repositoryMemberShip = new Lazy<IRepositoryMemberShip>(() => new RepositoryMemberShip(_context));
			_repositoryBlogImage = new Lazy<IRepositoryBlogImage>(() => new RepositoryBlogImage(_context));
			_repositoryStaff = new Lazy<IRepositoryStaff>(() => new RepositoryStaff(_context));
			_repositoryStaffAccount = new Lazy<IRepositoryStaffAccount>(() => new RepositoryStaffAccount(_context));
			_repositorySetting = new Lazy<IRepositorySetting>(() => new RepositorySetting(_context));
			_repositorySlider = new Lazy<IRepositorySlider>(() => new RepositorySlider(_context));
			_repositoryAnswer = new Lazy<IRepositoryAnswer>(() => new RepositoryAnswer(_context));
		}

		public IRepositorySetting Setting => _repositorySetting.Value;

		public IRepositoryBlogCategory BlogCategory => _repositoryBlogCategory.Value;

		public IRepositoryBlog Blog => _repositoryBlog.Value;

		public IRepositoryContact Contact => _repositoryContact.Value;

		public IRepositoryBlogImage BlogImage => _repositoryBlogImage.Value;

		public IRepositoryUser User => _repositoryUser.Value;

		public IRepositorySlider Slider => _repositorySlider.Value;

		public IRepositoryMemberShip MemberShip => _repositoryMemberShip.Value;
		public IRepositoryStaff Staff => _repositoryStaff.Value;
		public IRepositoryStaffAccount StaffAccount => _repositoryStaffAccount.Value;

        public IRepositoryAnswer Answer => _repositoryAnswer.Value;

        public void Save()
		{
			_context.SaveChanges();
		}
	}
}
