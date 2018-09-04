using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public AuctionService(IMapper mapper)
        {
            _auctionRepo = new AuctionRepo();
            _mapper = mapper;
        }

        public async Task<bool> CreateAuction(_AuctionCreate uim, string userId)
        {
            try
            {
                Auction dim  = _mapper.Map<Auction>(uim);
                dim.SkapadAv = userId;

                var success  = await _auctionRepo.CreateAuction(dim);
                return success;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> DeleteAuction(int AuctionId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAuction(_AuctionUpdate uim)
        {
            throw new NotImplementedException();
        }

        public async Task<_AuctionRead> GetAuction(int auctionId)
        {
            try
            {
                var auction = await _auctionRepo.GetAuction(auctionId);
                var result = _mapper.Map<_AuctionRead>(auction);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IList<_AuctionRead>> GetAuctions()
        {
            try
            {
                var auctions = await _auctionRepo.GetAuctions();
                var results = _mapper.Map<IList<_AuctionRead>>(auctions);
                return results;
            }
            catch
            {
                return null;
            }
        }
    }
}
