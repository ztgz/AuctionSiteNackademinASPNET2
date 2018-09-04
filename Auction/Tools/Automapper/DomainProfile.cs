﻿using Models.ViewModels;
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
            CreateMap<_AuctionCreate, Models.DataModels.Auction>()
                .ForMember(dto => dto.Gruppkod, opt => opt.UseValue(Models.DataModels.Auction.GROUP_CODE));
        }
    }
}
