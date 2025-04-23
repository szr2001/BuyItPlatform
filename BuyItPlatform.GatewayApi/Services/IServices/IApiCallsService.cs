using BuyItPlatform.GatewayApi.Models;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IApiCallsService
    {
        Task<ResponseDto> SendAsync(RequestDto request);
    }
}
