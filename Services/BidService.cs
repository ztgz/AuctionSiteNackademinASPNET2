using System.Collections.Generic;
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
        private readonly IMapper  _mapper;
        private readonly IBidRepo _bidRepo;

        public BidService(IMapper mapper, IBidRepo bidRepo)
        {
            _mapper  = mapper;
            _bidRepo = bidRepo;
        }

        public Task<IList<_BidRead>> GetBids(int auctionId)
        {
            throw new System.NotImplementedException();
        }

        public Task<(int statusCode, string message)> CreateBid(_BidManage uim, string userId)
        {
            try
            {
                var bid = _mapper.Map<Bid>(uim);
                bid.Budgivare = userId;
                
                await 

            }
            catch
            {
                return <int,string>(0, "Fel");
            }
        }
    }
}
