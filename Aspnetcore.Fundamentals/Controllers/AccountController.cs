using System.Threading.Tasks;
using Aspnetcore.Fundamentals.Models;
using Aspnetcore.Fundamentals.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aspnetcore.Fundamentals.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userMangager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userMangager, SignInManager<User> signInManager)
        {
            _userMangager = userMangager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User {UserName = model.Username};

                var createResult = await _userMangager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false); //isPersistent:false
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult =
                    await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Could not login");
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}