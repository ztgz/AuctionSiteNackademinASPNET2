using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Models.DataModels;
using Models.ViewModels;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepo _auctionRepo;
        private readonly IBidRepo     _bidRepo;
        private readonly IMapper _mapper;

        public AuctionService(IMapper mapper, IAuctionRepo auctionRepo, IBidRepo bidRepo)
        {
            _mapper      = mapper;
            _auctionRepo = auctionRepo;
            _bidRepo     = bidRepo;
        }

        #region Create
        public async Task<bool> CreateAuction(_AuctionManage uim, string userId)
        {
            bool success;

            try
            {
                Auction dim  = _mapper.Map<Auction>(uim);
                dim.SkapadAv = userId;

                success  = await _auctionRepo.CreateAuction(dim);
            }
            catch
            {
                success = false;
            }

            return success;
        }
        #endregion

        #region Read
        public async Task<_AuctionRead> GetAuction(int auctionId, string userId)
        {
            _AuctionRead result;
            try
            {
                var auction = await _auctionRepo.GetAuction(auctionId);
                result = _mapper.Map<_AuctionRead>(auction);
                result.IsOwner = auction.SkapadAv == userId;

                //set the maxbid if theres been bid
                var bids = await _bidRepo.GetBids(auctionId);
                if (bids.Count > 0)
                {
                    int maxBid = bids.Max(b => b.Summa);
                    var bid = bids.First(b => b.Summa == maxBid);
                    result.MaxBid = _mapper.Map<_BidRead>(bid);
                }
            }
            catch
            {
                result = null;
            }

            return result;
        }

        public async Task<IList<_AuctionRead>> GetAuctions(bool orderByStartingPrice)
        {
            IList<_AuctionRead> results;
            try
            {
                var auctions = await _auctionRepo.GetAuctions();
                results = _mapper.Map<IList<_AuctionRead>>(auctions);

                if (orderByStartingPrice)
                {
                    results =  results.OrderBy(r => r.Utropspris).ToList();
                }
                else
                {
                    results = results.OrderBy(a => a.SlutDatum).ToList();
                }
            }
            catch
            {
                results = null;
            }

            return results;
        }

        public async Task<IList<_AuctionRead>> GetAuctions(string searchTerm, bool orderByStartingPrice)
        {
            IList<_AuctionRead> results;
            try
            {
                searchTerm = searchTerm.ToLower();
                //Filter auctions on titel and beskrivning
                var auctions = (await _auctionRepo.GetAuctions()).
                    Where(a => a.Titel.ToLower().Contains(searchTerm) || a.Beskrivning.ToLower().Contains(searchTerm));
                results = _mapper.Map<IList<_AuctionRead>>(auctions);

                if (orderByStartingPrice)
                {
                    results = results.OrderBy(a => a.Utropspris).ToList();
                }
                else
                {
                    results = results.OrderBy(a => a.SlutDatum).ToList();
                }
            }
            catch
            {
                results = null;
            }

            return results;
        }

        public async Task<IList<_AuctionRead>> GetAuctions(int year, int month, string userId)
        {
            IList<_AuctionRead> results;
            try
            {
                //Filter the auctions
                var auctions = (await _auctionRepo.GetAuctions())
                    .Where(a => a.SlutDatum.Year == year && a.SlutDatum.Month == month);
                if (!string.IsNullOrEmpty(userId))
                {
                    auctions = auctions.Where(a => a.SkapadAv == userId);
                }

                results = _mapper.Map<IList<_AuctionRead>>(auctions);
               
            }
            catch
            {
                results = null;
            }

            return results;
        }

        #endregion

        #region Update
        public async Task<bool> UpdateAuction(_AuctionManage uim, string userId)
        {
            bool success;
            try
            {
                string createdBy = (await _auctionRepo.GetAuction(uim.AuktionId)).SkapadAv;
                //Only the user who created the auction can update it
                if (createdBy == userId)
                {
                    Auction dim = _mapper.Map<Auction>(uim);
                    dim.SkapadAv = userId;
                    success = await _auctionRepo.UpdateAuction(dim);
                }
                else
                {
                    success = false;
                }
            }
            catch
            {
                success = false;
            }

            return success;
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteAuction(int auctionId)
        {
            bool success;
            try
            {
                var bids = await _bidRepo.GetBids(auctionId);
                //Cannot remove auction if it still has bids
                if (bids.Count > 0)
                {
                    success = false;
                }
                else
                {
                    success = await _auctionRepo.DeleteAuction(auctionId);
                }
            }
            catch
            {
                success = false;
            }

            return success;
        }
        #endregion


    }
}
