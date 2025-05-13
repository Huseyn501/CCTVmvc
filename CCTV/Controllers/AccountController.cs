using CCTV.Helpers.Enums;
using CCTV.Models;
using CCTV.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CCTV.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _sign;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> sign,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _sign = sign;
            this.roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }
            AppUser appUser = new AppUser
            {
                UserName = registerVm.UserName,
                Email = registerVm.Email,
                Name = registerVm.Name,
                Surname = registerVm.Surname,
            };
            var result = await _userManager.CreateAsync(appUser, registerVm.Password);
            if (result.Succeeded)
            {
                await _sign.SignInAsync(appUser, true);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            await _userManager.AddToRoleAsync(appUser,UserRoles.Admin.ToString());
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> LogOut()
        {
            await _sign.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm,string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }
            AppUser appUser = await _userManager.FindByEmailAsync(loginVm.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View(loginVm);
            }
            var result = await _sign.CheckPasswordSignInAsync(appUser, loginVm.Password, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View(loginVm);
            }
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }
            await _sign.SignInAsync(appUser, true);
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> CreateRole()
        {
           foreach(var item in Enum.GetValues(typeof(UserRoles)))
            {
                await roleManager.CreateAsync(new IdentityRole(item.ToString()));
            }
           return RedirectToAction("Index", "Home");
        }
    }
}
