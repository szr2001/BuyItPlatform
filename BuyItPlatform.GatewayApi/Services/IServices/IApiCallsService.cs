using BuyItPlatform.GatewayApi.Models;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IApiCallsService
    {
        //generic so it's able to deserialize the Response property of the ResponseDTO
        //which can be LoginResponseDto, or a List of ListingsDto.
        Task<MicroserviceResponseDto<T>> SendAsync<T>(RequestDto request, bool withTokens = true);
    }
}
