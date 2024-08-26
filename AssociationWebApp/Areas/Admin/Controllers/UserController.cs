using AssociationWebApp.Models;
using Entities.Model;
using Entities.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.EFCore;

namespace AssociationWebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly IContactService _manager;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager /*IContactService manager*/)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_manager = manager;
        }
       
        public IActionResult Index(string? returnUrl)
        {
            if (returnUrl != null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View("~/Areas/Admin/Views/User/Index.cshtml");
        }



        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(viewModel.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, true);

                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);

                        var returnUrl = TempData["ReturnUrl"];
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl.ToString() ?? "/");
                        }

                        return RedirectToAction("Index", "User");
                    }
                    else if (result.RequiresTwoFactor)
                    {
                        return RedirectToAction("TwoFactorLogin", new { ReturnUrl = TempData["ReturnUrl"] });
                    }
                    else if (result.IsLockedOut)
                    {
                        var lockoutEndUtc = await _userManager.GetLockoutEndDateAsync(user);
                        var timeLeft = lockoutEndUtc.Value - DateTime.UtcNow;
                        ModelState.AddModelError(string.Empty, $"This account has been locked out, please try again {timeLeft.Minutes} minutes later.");
                    }
                    else if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "You need to confirm your e-mail address.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid e-mail or password.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid e-mail or password.");
                }
            }
            return View(viewModel);
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        //[HttpPost]
        //public async Task<IActionResult> Contact([FromForm] ContactDto contactDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _manager.contactService.CreateContact(contactDto);
        //        TempData["SuccessMessage"] = "Mesajınız Alınmıştır, Teşekkürler.";
        //        return RedirectToAction("Index");
        //    }
        //    return View(contactDto);
        //}

    }
}
