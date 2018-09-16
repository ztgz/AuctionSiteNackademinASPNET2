using System.Net;
using System.Threading.Tasks;
using Auction.Tools.AutoMapper;
using AutoMapper;
using Models.ViewModels;
using Repositories.Interfaces;
using Services;
using Services.Interfaces;
using Tests.MockClasses;
using Xunit;

namespace Tests
{
    public class TestBidService
    {
        private readonly IBidService _bidService;
        public TestBidService ()
        {
            IMapper mapper           = MockMapper.GetMapper();
            IBidRepo bidRepo         = new MockBidRepo();
            IAuctionRepo auctionRepo = new MockAuctionRepo();
            
            _bidService = new BidService(mapper, bidRepo, auctionRepo);
        }

        [Fact]
        public async Task GetBids()
        {
            var bids = await _bidService.GetBids(1);
            Assert.Equal(2, bids.Count);
            _BidRead bid = new _BidRead
            {
                AuktionId = 1,
                Budgivare = "Me",
                Summa = 1,
                BudID = 1,
            };

            Assert.True(_BidRead.AreEqual(bid, bids[1]));

            bid = new _BidRead
            {
                AuktionId = 1,
                Budgivare = "You",
                Summa = 2,
                BudID = 2,
            };
            Assert.True(_BidRead.AreEqual(bid, bids[0]));

        }

        [Fact]
        public async Task MaxBid()
        {
            int maxBid = await _bidService.GetMaxBid(1);
            Assert.Equal(2, maxBid);
        }

        [Fact]
        public async Task CreateBid()
        {
            var bid = new _BidManage()
            {
                AuktionID = 1,
                Summa = 2
            };

            var res = await _bidService.CreateBid(bid, "test");
            Assert.Equal((int)HttpStatusCode.BadRequest, res.statusCode);

            bid.Summa = 100;
            res = await _bidService.CreateBid(bid, "test");
            Assert.Equal((int)HttpStatusCode.NoContent, res.statusCode);
        }
    }
}
