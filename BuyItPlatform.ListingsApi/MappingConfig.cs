using AutoMapper;
using BuyItPlatform.ListingsApi.Models;
using BuyItPlatform.ListingsApi.Models.Dto;

namespace BuyItPlatform.ListingsApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration mappingConfig = new(config => 
            {
                config.CreateMap<ListingDto, Listing>();
                config.CreateMap<Listing, ListingDto>();
            });

            return mappingConfig;
        }
    }
}
