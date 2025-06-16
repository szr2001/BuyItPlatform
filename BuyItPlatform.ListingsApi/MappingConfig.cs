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
                config.CreateMap<ListingUploadDto, Listing>();
                config.CreateMap<Listing, ListingViewDto>();
            });

            return mappingConfig;
        }
    }
}
