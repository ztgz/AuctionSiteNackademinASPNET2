using System;
using System.Collections.Generic;
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
        private const string ROLE_ADMIN   = "Admin";
        private const string ROLE_REGULAR = "Regular";

        private readonly IAuctionService      _auctionService;
        private readonly UserManager<AppUser> _userManager;

        public AuctionController (IAuctionService auctionService, UserManager<AppUser> userManager)
        {
            _auctionService = auctionService;
            _userManager    = userManager;
        }

        #region GetAuctions

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var auctions = await _auctionService.GetAuctions();
            return View(auctions);
        }
        #endregion

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAuction(int auctionId)
        {
            var auction = await _auctionService.GetAuction(auctionId);
            return View(auction);
        }

        #region Create Auction
        [HttpGet]
        [Authorize(Roles = ROLE_ADMIN)]
        public IActionResult CreateAuction()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ROLE_ADMIN)]
        public async Task<IActionResult> CreateAuction(_AuctionCreate uim)
        {
            try
            {
                if (uim.StartDatum < DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, "Startid måste komma efter nuvarande tid");
                }
                else if (uim.StartDatum >= uim.SlutDatum)
                {
                    ModelState.AddModelError(string.Empty, "Starttid måste komma före sluttid");
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
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Internt fel, kunde inte skapa ny auktion");
                return View();
            }

            return View(uim);
        }

        [HttpGet]
        [Authorize(Roles = ROLE_ADMIN)]
        public IActionResult CreatedAuction()
        {
            return View();
        }
        #endregion

    }
}