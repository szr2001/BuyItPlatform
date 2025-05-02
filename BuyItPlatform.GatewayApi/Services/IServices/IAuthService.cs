using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto<T>> LoginUser<T>(LoginRequestDto registerData);
        Task<ResponseDto<T>> RegisterUser<T>(RegisterRequestDto registerData);
        Task<ResponseDto<T>> AssignRole<T>(string email, string rolename);
        Task<ResponseDto<T>> RefreshToken<T>(RefreshTokenRequest request);
    }
}
