using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;

namespace BuyItPlatform.GatewayApi.Service.IService
{
    public interface IUserService
    {
        Task<ResponseDto<T>> GetUserProfile<T>(string userId);
    }
}
