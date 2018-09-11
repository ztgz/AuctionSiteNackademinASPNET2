using Models.ViewModels;
using Models.DataModels;
using AutoMapper;

namespace Auction.Tools.AutoMapper
{
    //When the application runs, automapper will go through the code and looking for classes that
    //inherits from profile and lood the specified configuration
    public class DomainProfile : Profile
    {
        public DomainProfile ()
        {
            CreateMap<Models.DataModels.Auction, _AuctionRead>();
            CreateMap<_AuctionManage, Models.DataModels.Auction>()
                .ForMember(dto => dto.Gruppkod, opt => opt.UseValue(Models.DataModels.Auction.GROUP_CODE));
            CreateMap<_BidManage, Bid>();
            CreateMap<Bid, _BidRead>();
        }
    }
}
