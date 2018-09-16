using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Models.ViewModels;
using Repositories.Interfaces;
using Services;
using Services.Interfaces;
using Tests.MockClasses;
using Xunit;

namespace Tests
{
    public class TestAuctionService
    {
        private readonly IAuctionService _auctionService;
        public TestAuctionService ()
        {
            IMapper mapper = MockMapper.GetMapper();
            IBidRepo bidRepo = new MockBidRepo();
            IAuctionRepo auctionRepo = new MockAuctionRepo();
            _auctionService = new AuctionService(mapper, auctionRepo, bidRepo);
        }

        [Fact]
        public async Task CreateAuction()
        {
            _AuctionManage uim = new _AuctionManage
            {
                Titel = "Title",
                StartDatum = new DateTime(2018,02,02,00,00,00),
                SlutDatum = new DateTime(2018,01,01,00,00,00),
                Beskrivning = "Beskrivning",
                Utropspris = 1
            };

            //Start date before end date
            var res = await _auctionService.CreateAuction(uim, "1");
            Assert.False(res);

            //Should work
            uim.SlutDatum = new DateTime(2028, 01, 01, 00, 00, 00);
            res = await _auctionService.CreateAuction(uim, "1");
            Assert.True(res);

            //Start date before end date
            uim.Utropspris = 0;
            res = await _auctionService.CreateAuction(uim, "1");
            Assert.False(res);

        }

        [Fact]
        public async Task DeleteAuction()
        {
            //Does have bid
            var res = await _auctionService.DeleteAuction(1);
            Assert.False(res);

            //Should work
            res = await _auctionService.DeleteAuction(2);
            Assert.True(res);
        }

        
        [Fact]
        public async Task UpdateAuction()
        {
            _AuctionManage uim = new _AuctionManage
            {
                Titel = "Title",
                StartDatum = new DateTime(2018,02,02,00,00,00),
                SlutDatum = new DateTime(2018,01,01,00,00,00),
                Beskrivning = "Beskrivning",
                Utropspris = 1,
                AuktionId = 1
            };

            //Wrong user id
            var res = await _auctionService.UpdateAuction(uim, "You");
            Assert.False(res);

            //Correct user id
            res = await _auctionService.UpdateAuction(uim, "Me");
        }

        [Fact]
        public async Task GetAuction()
        {
            _AuctionRead auction = new _AuctionRead
            {
                AuktionId = 1,
                Titel = "Titel",
                Beskrivning = "Beskrivning",
                StartDatum = new DateTime(2018, 01, 01, 10, 10, 10),
                SlutDatum = new DateTime(2040, 02, 01, 10, 10, 10),
                Utropspris = 1,
                MaxBid = new _BidRead{Summa = 2},
                IsOwner = true
            };

            //working
            var res = await _auctionService.GetAuction(1, "Me");
            Assert.True(_AuctionRead.AreEqual(auction, res));

            //working
            auction.IsOwner = false;
            res = await _auctionService.GetAuction(1, "You");
            Assert.True(_AuctionRead.AreEqual(auction, res));

            //not the owner
            auction.IsOwner = true;
            res = await _auctionService.GetAuction(1, "You");
            Assert.False(_AuctionRead.AreEqual(auction, res));

        }

        [Fact]
        public async Task GetAuctions()
        {
            var auctions = await _auctionService.GetAuctions(false);
            Assert.Equal(2, auctions.Count);

            _AuctionRead auction = new _AuctionRead
            {
                AuktionId = 2,
                Titel = "Titel",
                Beskrivning = "Beskrivning",
                StartDatum = new DateTime(2018, 01, 01, 10, 10, 10),
                SlutDatum = new DateTime(2018, 02, 01, 10, 10, 10),
                Utropspris = 1,
                MaxBid = null,
                IsOwner = false
            };
            Assert.True(_AuctionRead.AreEqual(auction, auctions[0]));
        }

    }
}
