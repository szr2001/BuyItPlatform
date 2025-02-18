using AutoMapper;
using BuyItPlatform.AuthApi.Models;
using BuyItPlatform.AuthApi.Models.Dto;

namespace BuyItPlatform.AuthApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration mappingConfig = new(config => 
            {
                config.CreateMap<BuyItUser, UserDto>();
            });

            return mappingConfig;
        }
    }
}
