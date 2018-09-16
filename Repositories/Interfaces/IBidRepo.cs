using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataModels;

namespace Repositories.Interfaces
{
    public interface IBidRepo
    {
        Task<IList<Bid>> GetBids(int auctionId);
        Task<bool>       CreateBid(Bid dim);
        Task<bool>       DeleteBid(int bidId);
    }
}
