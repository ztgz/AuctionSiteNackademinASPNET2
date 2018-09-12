using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.IdentityModels;

namespace Auction.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public AdminController (UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var a = (await _userManager.GetUsersInRoleAsync(AppUser.ROLE_ADMIN)).ToList();
            var b = (await _userManager.GetUsersInRoleAsync(AppUser.ROLE_REGULAR)).ToList();
            return View();
        }
    }
}