using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.IdentityModels;
using Services.Interfaces;

namespace Auction.Controllers
{
    [Authorize(Roles = AppUser.ROLE_ADMIN)]
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
            bool result = false;
            //User cannot change it's role
            if (User.Identity.Name != userEmail)
            {
                result = await _accountService.SetUserToRole(userEmail, role);
            } 
            return new JsonResult(result);
        }
    }
}