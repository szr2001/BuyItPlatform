using AutoMapper;

namespace BuyItPlatform.AuthApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration mappingConfig = new(config => 
            {
            });

            return mappingConfig;
        }
    }
}
