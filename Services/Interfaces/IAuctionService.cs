using System.Collections.Generic;
using System.Threading.Tasks;
using Models.ViewModels;

namespace Services.Interfaces
{
    public interface IAuctionService
    {
        Task<bool>                CreateAuction(_AuctionManage uim, string userId);
        Task<bool>                DeleteAuction(int auctionId);
        Task<bool>                UpdateAuction(_AuctionManage uim, string userId);
        Task<_AuctionRead>        GetAuction(int auctionId, string userId);
        Task<IList<_AuctionRead>> GetAuctions(bool orderByStartingPrice);
        Task<IList<_AuctionRead>> GetAuctions(string searchTerm, bool orderByStartingPrice);

    }
}
