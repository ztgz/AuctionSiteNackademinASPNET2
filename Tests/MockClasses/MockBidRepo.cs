using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models.DataModels;
using Repositories.Interfaces;

namespace Tests.MockClasses
{
    public class MockBidRepo : IBidRepo
    {
        public Task<IList<Bid>> GetBids(int auctionId)
        {
            IList<Bid> bids = new List<Bid>();

            bids.Add(new Bid()
            {
                AuktionId = 1,
                Budgivare = "Me",
                Summa = 1,
                BudID = 1,
            });

            bids.Add(new Bid()
            {
                AuktionId = 1,
                Budgivare = "You",
                Summa = 2,
                BudID = 2,
            });

            return Task.FromResult(bids);
        }

        public Task<bool> CreateBid(Bid dim)
        {
            return Task.FromResult(true);
        }

        public Task<bool> DeleteBid(int bidId)
        {
            return Task.FromResult(true);
        }
    }
}
