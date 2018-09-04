using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataModels;

namespace Repositories.Interfaces
{
    public interface IAuctionRepo
    {
        Task<bool>           CreateAuction(Auction dim);
        Task<bool>           DeleteAuction(int auctionId);
        Task<bool>           UpdateAuction(Auction dim);
        Task<Auction>        GetAuction(int auctionId);
        Task<IList<Auction>> GetAuctions();
    }
}
