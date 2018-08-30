using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;

namespace Auction.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM uim)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {
                ModelState.AddModelError("", "Du måste ange både email och lösenord");
            }
            return View(uim);
        }
        
    }
}