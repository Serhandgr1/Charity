using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Repositories.EFCore
{
	public class RepositoryContext : IdentityDbContext<User>
	{
		public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
		{

		}
		public DbSet<User> Users { get; set; }
      //  public DbSet<Role> Roles { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
		public DbSet<StaffAccount> StaffAccounts { get; set; }
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<BlogCategory> BlogCategories { get; set; }
		public DbSet<BlogImage> BlogImages { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Setting> Settings { get; set; }
		public DbSet<Slider> Sliders { get; set; }
		public DbSet<Staff> Staffs { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }
		public DbSet<Survey> Surveys { get; set; }	

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-LSURQLJ\\SQLEXPRESS;Database=AssociationWebApp;Trusted_Connection=True;MultipleActiveResultSets=true");
                //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
