using AutoMapper;
using BuyItPlatform.CommentsApi.Models;
using BuyItPlatform.CommentsApi.Models.Dto;

namespace BuyItPlatform.CommentsApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration mappingConfig = new(config => 
            {
                config.CreateMap<CommentDto, Comment>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
