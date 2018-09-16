using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repositories.Interfaces;
using Auction = Models.DataModels.Auction;

namespace Tests.MockClasses
{
    public class MockAuctionRepo : IAuctionRepo
    {
        public Task<bool> CreateAuction(Models.DataModels.Auction dim)
        {
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAuction(int auctionId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> UpdateAuction(Models.DataModels.Auction dim)
        {
            return Task.FromResult(true);
        }

        public Task<Models.DataModels.Auction> GetAuction(int auctionId)
        {
            var auction = new Models.DataModels.Auction
            {
                AuktionId = 1,
                Titel = "Titel",
                Beskrivning = "Beskrivning",
                StartDatum = new DateTime(2018, 01, 01, 10, 10, 10),
                SlutDatum = new DateTime(2040, 02, 01, 10, 10, 10),
                Utropspris = 1,
                SkapadAv = "Me",
                Gruppkod = 1
            };

            return Task.FromResult(auction);
        }

        public Task<IList<Models.DataModels.Auction>> GetAuctions()
        {

            IList<Models.DataModels.Auction> auctions = new List<Models.DataModels.Auction>();
            auctions.Add(new Models.DataModels.Auction
            {
                AuktionId = 1,
                Titel = "Titel",
                Beskrivning = "Beskrivning",
                StartDatum = new DateTime(2018, 01, 01, 10, 10, 10),
                SlutDatum = new DateTime(2040, 02, 01, 10, 10, 10),
                Utropspris = 1,
                SkapadAv = "Me",
                Gruppkod = 1
            });

            auctions.Add(new Models.DataModels.Auction
            {
                AuktionId = 2,
                Titel = "Titel",
                Beskrivning = "Beskrivning",
                StartDatum = new DateTime(2018, 01, 01, 10, 10, 10),
                SlutDatum = new DateTime(2018, 02, 01, 10, 10, 10),
                Utropspris = 1,
                SkapadAv = "You",
                Gruppkod = 1
            });

            return Task.FromResult(auctions);
        }
    }
}
