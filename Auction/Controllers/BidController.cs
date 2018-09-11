using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.IdentityModels;
using Models.ViewModels;
using Services.Interfaces;

namespace Auction.Controllers
{
    [Authorize]
    public class BidController : Controller
    {
        private readonly IBidService _bidService;
        private readonly UserManager<AppUser> _userManager;

        public BidController (IBidService bidService, UserManager<AppUser> userManager)
        {
            _bidService  = bidService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> CreateBid(int id)
        {
            int maxBid = await _bidService.GetMaxBid(id);
            var model = new _BidManage()
            {
                AuktionID = id,
                Summa     = maxBid+1
            };

            return PartialView("_CreateBid", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBid(_BidManage uim)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                //Get the reposnse...
                var response = await _bidService.CreateBid(uim, userId);
                //...and return it;
                return StatusCode(response.statusCode, response.message);
            }

            string errorResponse = "";
            //Add and return all the error messages from modelstate
            foreach (var msValues in ModelState.Values)
            {
                foreach (var error in msValues.Errors)
                {
                    errorResponse +=  error.ErrorMessage + ". ";
                }
            }
       
            return StatusCode((int)HttpStatusCode.BadRequest, errorResponse);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBids(int id)
        {
            var bids = await _bidService.GetBids(id);
            return PartialView("_ListBids", bids);
        }
    }
}