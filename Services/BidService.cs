using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Models.DataModels;
using Models.ViewModels;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class BidService : IBidService
    {
        private readonly IMapper      _mapper;
        private readonly IBidRepo     _bidRepo;
        private readonly IAuctionRepo _auctionRepo;

        public BidService(IMapper mapper, IBidRepo bidRepo, IAuctionRepo auctionRepo)
        {
            _mapper      = mapper;
            _bidRepo     = bidRepo;
            _auctionRepo = auctionRepo;
        }

        public async Task<IList<_BidRead>> GetBids(int auctionId)
        {
            try
            {
                var bids = (await _bidRepo.GetBids(auctionId)).OrderByDescending(b => b.Summa);
                var result = _mapper.Map<IList<_BidRead>>(bids);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> GetMaxBid(int auctionId)
        {
            try
            {
                var bids = await _bidRepo.GetBids(auctionId);
                //Returns zero if no bids are found
                return bids.Max(b => b.Summa as int?) ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<(int statusCode, string message)> CreateBid(_BidManage uim, string userId)
        {
            //The return response
            (int, string) responseMessage;

            try
            {
                var auction = await _auctionRepo.GetAuction(uim.AuktionID);
                //Validate the bid against the auction
                if (auction == null)
                {
                    responseMessage = ((int) HttpStatusCode.NotFound, "Kan inte hitta auktion");
                }
                else if (auction.StartDatum > DateTime.Now)
                {
                    responseMessage = ((int) HttpStatusCode.BadRequest, "Auktionen har inte startat ännu");
                }
                else if (auction.SlutDatum < DateTime.Now)
                {
                    responseMessage = ((int) HttpStatusCode.BadRequest, "Auktionen är över");
                }
                else if (auction.Utropspris > uim.Summa)
                {
                    responseMessage = ((int) HttpStatusCode.BadRequest, "Bud måste vara över utropspris");
                }
                //Validate that bid is more than maxbid of the auction
                else if (await GetMaxBid(uim.AuktionID) >= uim.Summa)
                {
                    responseMessage = ((int) HttpStatusCode.BadRequest, "Bud måste vara större än nuvarande högstabud");
                }
                //Try to make the bid
                else
                {
                    var dim = _mapper.Map<Bid>(uim);
                    dim.Budgivare = userId;
                    var result = await _bidRepo.CreateBid(dim);

                    //If the bid was created...
                    if (result)
                    {
                        //...return success status code
                        responseMessage = ((int) HttpStatusCode.NoContent, "Bud lyckades");
                    }
                    else
                    {
                        //...otherwise return a fail code
                        responseMessage = ((int) HttpStatusCode.InternalServerError, "Kunde inte lägga bud");
                    }
                }
            }
            catch   
            {
                responseMessage = ((int) HttpStatusCode.InternalServerError, "Något gick fel på servern");
            }

            return responseMessage;
        }
    }
}
