using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.IdentityModels;
using Models.ViewModels.Statistics;
using Newtonsoft.Json;
using Services.Interfaces;

namespace Auction.Controllers
{
    [Authorize(Roles = AppUser.ROLE_ADMIN)]
    public class StatisticsController : Controller
    {
        private readonly IStatisticService _statisticService;
        private readonly UserManager<AppUser> _userManager;
        public StatisticsController (IStatisticService statisticService, UserManager<AppUser> userManager)
        {
            _statisticService = statisticService;
            _userManager      = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<JsonResult> GetBarChart(bool fromAll, DateTime startDate, DateTime endDate)
        {
            _BarChart barChart ;
            if (fromAll)
            {
               barChart = await _statisticService.GetMonthlySummary(startDate, endDate);
            }
            else
            {
                string userId = _userManager.GetUserId(User);
                barChart = await _statisticService.GetMonthlySummary(startDate, endDate, userId);
            }

            return Json(barChart);
        }
    }
}