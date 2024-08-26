using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AssociationWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Entities.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Entities.ModelDto;
using Microsoft.AspNetCore.Authentication;
using System.Data;

namespace AssociationWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private User? _user;
        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _user = await _userManager.FindByNameAsync(viewModel.UserName);
                if (_user != null)
                {
                   await _signInManager.SignOutAsync();
                    var result = await _userManager.CheckPasswordAsync(_user, viewModel.Password);

                    if (result)
                    {
                        var roles = await _userManager.GetRolesAsync(_user);

                        //await _userManager.ResetAccessFailedCountAsync(user);
                        //await _userManager.SetLockoutEndDateAsync(user, null);

                        var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, _user.UserName)
                        };
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = viewModel.RememberMe
                        };
                        var userIdentity= new ClaimsIdentity(claims,"Login");
                       // ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(userIdentity);
                       // await HttpContext.SignInAsync(claimsPrincipal);
                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(userIdentity),
            authProperties);




                        var returnUrl = TempData["ReturnUrl"];
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl.ToString() ?? "/");
                        }

                        return RedirectToAction("Index", "User");
                    }
                    //else if (result.RequiresTwoFactor)
                    //{
                    //    return RedirectToAction("TwoFactorLogin", new { ReturnUrl = TempData["ReturnUrl"] });
                    //}
                    //else if (result.IsLockedOut)
                    //{
                    //    var lockoutEndUtc = await _userManager.GetLockoutEndDateAsync(user);
                    //    var timeLeft = lockoutEndUtc.Value - DateTime.UtcNow;
                    //    ModelState.AddModelError(string.Empty, $"This account has been locked out, please try again {timeLeft.Minutes} minutes later.");
                    //}
                    //else if (result.IsNotAllowed)
                    //{
                    //    ModelState.AddModelError(string.Empty, "You need to confirm your e-mail address.");
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError(string.Empty, "Invalid e-mail or password.");
                    //}
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid e-mail or password.");
                }
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
         
           var user = new User // User türünde bir nesne oluşturuluyor
            {
                UserName = model.UserName,
                Email = model.Email
            };
            string[] rol = { "Admin" };
            model.Roles = rol;
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) await _userManager.AddToRolesAsync(user, model.Roles);
          //  var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Rol ekleme işlemi kaldırıldı
                TempData["message"] = "Kayıt işleminiz başarıyla tamamlandı. Giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }
    }
}






