using AssociationWebApp.Extension;
using AssociationWebApp.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Repositories.AutoMapper;
using Repositories.EFCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddLogging(log =>
{
	log.ClearProviders();
	log.AddFile($"{Directory.GetCurrentDirectory()}\\LogFile\\log.txt", LogLevel.Error);
});
builder.Services.AddRazorPages().AddViewLocalization();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.ConfiguratioSQLContext(builder.Configuration);
builder.Services.AddSingleton<LocalizationMiddleware>();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.ConfiguerRepostoryManager();
builder.Services.ConfiguerServiceManager();
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization();
builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureApplicationCookie();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	var supportedCultures = new[]
	{
		new CultureInfo("kr-KR"),
		new CultureInfo("en-US"),
		new CultureInfo("tr-TR")
	};

	options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
	options.DefaultRequestCulture = new RequestCulture(new CultureInfo("kr-KR"));
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;
});
builder.Services.AddControllers()
		.AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.PropertyNamingPolicy = null; // Disable camelCase
		});

//builder.Services.AddSingleton(x =>
//	new PaypalClient(
//		builder.Configuration["PayPalOptions:ClientId"],
//		builder.Configuration["PayPalOptions:ClientSecret"],
//		builder.Configuration["PayPalOptions:Mode"]
//	)
//);
builder.Services.AddDbContext<RepositoryContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("AssociationWebAppContextConnection"));
});
//builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<RepositoryContext>();
//builder.Services.ConfigureApplicationCookie(options =>
//{
//	options.LoginPath = new PathString("/User/Login");
//	options.LogoutPath = new PathString("/User/Logout");
//	options.AccessDeniedPath = new PathString("/User/Login");

//	options.Cookie = new()
//	{
//		Name = "IdentityCookie",
//		HttpOnly = true,
//		SameSite = SameSiteMode.Lax,
//		SecurePolicy = CookieSecurePolicy.Always
//	};
//	options.SlidingExpiration = true;
//	options.ExpireTimeSpan = TimeSpan.FromDays(30);
//});
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
				.AddCookie(options =>
				{
					options.LoginPath = "/Admin/Login";
					options.LogoutPath = "/User/Logout";
					options.AccessDeniedPath = "/Admin/Login";
				});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var localizationOptions = services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
	app.UseRequestLocalization(localizationOptions);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<LocalizationMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
	name: "Admin",
	areaName: "Admin",
	pattern: "{controller=Admin}/{action=Login}/{id?}");

app.Run();
