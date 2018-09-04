using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataModels;
using Models.ViewModels;

namespace Services.Interfaces
{
    public interface IAuctionService
    {
        Task<bool>                CreateAuction(_AuctionCreate uim, string userId);
        Task<bool>                DeleteAuction(int auctionId);
        Task<bool>                UpdateAuction(_AuctionUpdate uim);
        Task<_AuctionRead>        GetAuction(int auctionId);
        Task<IList<_AuctionRead>> GetAuctions();
    }
}
