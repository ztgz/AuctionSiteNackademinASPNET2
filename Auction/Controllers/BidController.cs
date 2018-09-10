using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Services.Interfaces;

namespace Auction.Controllers
{
    public class BidController : Controller
    {
        private readonly IBidService _bidService;
        public BidController (IBidService bidService)
        {
            _bidService = bidService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBid(_BidManage uim)
        {
            if (ModelState.IsValid)
            {
                var success = await _bidService.CreateBid(uim);
                return RedirectToAction("GetAuction", "Auction", uim.AuktionID);
            }
            return PartialView("_CreateBid", uim);
        }
    }
}