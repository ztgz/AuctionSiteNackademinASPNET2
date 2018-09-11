using System.Collections.Generic;
using System.Threading.Tasks;
using Models.ViewModels;

namespace Services.Interfaces
{
    public interface IBidService
    {
        Task<IList<_BidRead>>                  GetBids(int auctionId);
        Task<int>                              GetMaxBid(int auctionId);
        Task<(int statusCode, string message)> CreateBid(_BidManage uim, string userId);
    }
}
