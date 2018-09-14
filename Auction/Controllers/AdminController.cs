using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.IdentityModels;
using Services.Interfaces;

namespace Auction.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAccountService _accountService;
        public AdminController (IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _accountService.GetUsersRoles();
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> ChangeRole(string userEmail, string role)
        {
            var result = await _accountService.SetUserToRole(userEmail, role);
            return new JsonResult(result);
        }
    }
}