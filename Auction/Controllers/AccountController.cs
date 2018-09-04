using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.IdentityModels;
using Models.ViewModels;

namespace Auction.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private const string ROLE_ADMIN = "Admin";
        private const string ROLE_REGULAR  = "Regular";

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController (SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager   = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Auction");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(_UserLogin uim)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(uim.Email, uim.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Auction");
                }
                ModelState.AddModelError(string.Empty, "Felaktig login");
            }

            return View(uim);
        }

        [AllowAnonymous]
        public IActionResult LoginFacebook()
        {
            //var abc = Challenge(new AuthenticationProperties { RedirectUri = "/" }, "Facebook");
            var redirectUrl = Url.Action("FacebookLoginCallback", "Account");
            var properties =_signInManager.ConfigureExternalAuthenticationProperties(FacebookDefaults.AuthenticationScheme, redirectUrl);
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> FacebookLoginCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login", "Account");
            }

            //Try to sign in user
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, true);
            //If the user has an account
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Auction");
            }
            //Otherwise...
            else
            {
                //...create an account
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                AppUser user = new AppUser{ UserName = email, Email = email };

                var identityResult = await _userManager.CreateAsync(user);
                if (identityResult.Succeeded)
                {
                    identityResult = await _userManager.AddLoginAsync(user, info);
                    if (identityResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, ROLE_REGULAR);
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Auction");
                    }
                }
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Auction");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(_UserRegister uim)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = uim.Email, Email = uim.Email };
                var result = await _userManager.CreateAsync(user, uim.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, ROLE_REGULAR);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Auction");
                }
                AddErrors(result);
            }

            return View(uim);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}