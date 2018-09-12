using System;
using System.Threading.Tasks;
using Models.ViewModels.Statistics;

namespace Services.Interfaces
{
    public interface IStatisticService
    {
        Task<_BarChart> GetMonthlySummary(DateTime startDate, DateTime endDate, string userId);
        Task<_BarChart> GetMonthlySummary(DateTime startDate, DateTime endDate);
    }
}
