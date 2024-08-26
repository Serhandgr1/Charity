using Entities.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using Services.EFCore;
using System.Reflection;


namespace AssociationWebApp.Extension
{
	public static class ServiceExtension
	{
		public static void ConfiguratioSQLContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Repositories")));
		}
		public static void ConfiguerRepostoryManager(this IServiceCollection services)
		{

			//Repo base entitlere göre düzenlenecek hepsi eklenecek 


			//services.AddScoped<IdentityUser, IdentityUser>();
			services.AddScoped<IRepositoryBlog, RepositoryBlog>();
			services.AddScoped<IRepositoryBlogCategory, RepositoryBlogCategory>();
			services.AddScoped<IRepositoryContact, RepositoryContact>();
			services.AddScoped<IRepositoryBlogImage, RepositoryBlogImage>();
			services.AddScoped<IRepositorySetting, RepositorySetting>();
			services.AddScoped<IRepositoryUser, RepositoryUser>();
			services.AddScoped<IRepositoryMemberShip, RepositoryMemberShip>();
			services.AddScoped<IRepositorySlider, RepositorySlider>();
			services.AddScoped<IRepositoryManager, RepositoryManager>();
			services.AddScoped<IRepositoryStaff, RepositoryStaff>();
			services.AddScoped<IRepositoryStaffAccount, RepositoryStaffAccount>();
            services.AddScoped<IRepositoryQuestion, RepositoryQuestion>();
            services.AddScoped<IRepositoryAnswer, RepositoryAnswer>();
            services.AddScoped<IRepositorySurvey, RepositorySurvey>();

            //repostory Referanslar
            services.AddScoped<IRepositoryBase<Blog>, RepositoryBase<Blog>>();
			services.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
			services.AddScoped<IRepositoryBase<BlogImage>, RepositoryBase<BlogImage>>();
			services.AddScoped<IRepositoryBase<Contact>, RepositoryBase<Contact>>();
			services.AddScoped<IRepositoryBase<Slider>, RepositoryBase<Slider>>();
			services.AddScoped<IRepositoryBase<Setting>, RepositoryBase<Setting>>();
			services.AddScoped<IRepositoryBase<BlogCategory>, RepositoryBase<BlogCategory>>();
			services.AddScoped<IRepositoryBase<MemberShip>, RepositoryBase<MemberShip>>();
			services.AddScoped<IRepositoryBase<Staff>, RepositoryBase<Staff>>();
			services.AddScoped<IRepositoryBase<StaffAccount>, RepositoryBase<StaffAccount>>();
			services.AddScoped<IRepositoryBase<Survey>, RepositoryBase<Survey>>();
		}
		public static void ConfiguerServiceManager(this IServiceCollection services)
		{ // service referanslar
			services.AddScoped<IBlogService, BlogService>();
			services.AddScoped<IBlogCategoryService, BlogCategoryService>();
			services.AddScoped<IBlogImageService, BlogImageService>();
			services.AddScoped<IContactService, ContactService>();
			services.AddScoped<ISettingService, SettingService>();
			services.AddScoped<IServiceManager, ServiceManager>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ISliderService, SliderService>();
			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<IMemberShipService, MemberShipService>();
			services.AddScoped<IStaffService, StaffService>();
			services.AddScoped<IStaffAccountService, StaffAccountService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();
        }
		public static void ConfigureIdentity(this IServiceCollection services)
		{
			var builder = services.AddIdentity<User, IdentityRole>
				(
					opts =>
					{
						opts.Password.RequireDigit = true;
						opts.Password.RequireLowercase = true;
						opts.Password.RequireUppercase = true;
						opts.Password.RequireNonAlphanumeric = true;
						opts.Password.RequiredLength = 8;
						opts.User.RequireUniqueEmail = true;

					}
				).AddEntityFrameworkStores<RepositoryContext>()
				.AddDefaultTokenProviders();
		}
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			return services;
		}
        public static void ConfigureApplicationCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Login");
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
        }


    }
}
