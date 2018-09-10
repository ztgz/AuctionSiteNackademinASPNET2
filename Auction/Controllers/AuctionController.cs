using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AuctionController : Controller
    {
        private readonly IAuctionService _auctionService;
        private readonly UserManager<AppUser> _userManager;

        public AuctionController(IAuctionService auctionService, UserManager<AppUser> userManager)
        {
            _auctionService = auctionService;
            _userManager = userManager;
        }

        #region Get
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //Allways order auctions on enddate as default
            var auctions = await _auctionService.GetAuctions(orderByStartingPrice: false);
            //TODO IF AUCTION == null
            if (auctions == null)
            {
                auctions = new List<_AuctionRead>();
            }
            return View(auctions);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAuction(int auctionId)
        {
            var userId = _userManager.GetUserId(User);
            var auction = await _auctionService.GetAuction(auctionId, userId);
            //TODO IF AUCTION == null

            return View(auction);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string searchTerm, bool orderByStartingPrice)
        {
            searchTerm = searchTerm ?? "";
            var auctions = await _auctionService.GetAuctions(searchTerm, orderByStartingPrice);
            //TODO IF AUCTION == null
            if (auctions == null)
            {
                auctions = new List<_AuctionRead>();
            }
            return PartialView("_AuctionList", auctions);
        }
        
        #endregion

        #region Create
        [HttpGet]
        [Authorize(Roles = AppUser.ROLE_ADMIN)]
        public IActionResult CreateAuction()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppUser.ROLE_ADMIN)]
        public async Task<IActionResult> CreateAuction(_AuctionManage uim)
        {
            //Validates that the model is correctly specified
            IList<string> errors = uim.Validate();
            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(String.Empty, error);
                }
            }
            else if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                bool success = await _auctionService.CreateAuction(uim, userId);
                if (success)
                {
                    return RedirectToAction("CreatedAuction", "Auction");
                }
                // Det gick inte att skapa en auktion
                ModelState.AddModelError(string.Empty, "Kunde inte skapa ny auktion");
            }

            return View(uim);
        }

        [HttpGet]
        [Authorize(Roles = AppUser.ROLE_ADMIN)]
        public IActionResult CreatedAuction()
        {
            return View();
        }
        #endregion

        #region Update
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAuction(_AuctionManage uim)
        {
            //TODO FIXA FEEDBACK TILL ANVÄNDAREN
            //Validates that the model is correctly specified
            IList<string> errors = uim.Validate();
            //If there are errors
            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(String.Empty, error);
                }
            }
            else if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                bool success = await _auctionService.UpdateAuction(uim, userId);
                if (success)
                {
                    return RedirectToAction("GetAuction", "Auction", new { auctionId = uim.AuktionId });
                }
                ModelState.AddModelError(string.Empty, "Kunde inte uppdatera auktion " + uim.AuktionId);
            }
            
            return RedirectToAction("GetAuction", "Auction", new { auctionId = uim.AuktionId });
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppUser.ROLE_ADMIN)]
        public async Task<IActionResult> DeleteAuction(int auctionId)
        {
            bool success = await _auctionService.DeleteAuction(auctionId);
            if (success)
            {
                return RedirectToAction("Index", "Auction");
            }

            return RedirectToAction("GetAuction", "Auction", new { auctionId = auctionId });
        }
        #endregion


    }
}