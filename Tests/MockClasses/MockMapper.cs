using Auction.Tools.AutoMapper;
using AutoMapper;

namespace Tests.MockClasses
{
    public static class MockMapper
    {
        public static IMapper GetMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainProfile());
            });
            IMapper mapper = mapperConfiguration.CreateMapper();

            return mapper;
        }
    }
}
