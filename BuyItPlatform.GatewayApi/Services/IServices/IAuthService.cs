using BuyItPlatform.GatewayApi.Models;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IAuthService
    {
        Task<MicroserviceResponseDto<T>> LoginUser<T>(LoginRequestDto registerData);
        Task<MicroserviceResponseDto<T>> RegisterUser<T>(RegisterRequestDto registerData);
        Task<MicroserviceResponseDto<T>> AssignRole<T>(string email, string rolename);
        Task<MicroserviceResponseDto<T>> RefreshToken<T>();
        Task<MicroserviceResponseDto<T>> GetUserProfile<T>(string userId);
        Task<MicroserviceResponseDto<T>> Logout<T>();
    }
}
