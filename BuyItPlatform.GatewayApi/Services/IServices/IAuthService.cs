using BuyItPlatform.GatewayApi.Models;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IAuthService
    {
        Task<MicroserviceResponseDto<T>> LoginUserAsync<T>(LoginRequestDto registerData);
        Task<MicroserviceResponseDto<T>> RegisterUserAsync<T>(RegisterRequestDto registerData);
        Task<MicroserviceResponseDto<T>> AssignRoleAsync<T>(string email, string rolename);
        Task<MicroserviceResponseDto<T>> RefreshTokenAsync<T>();
        Task<MicroserviceResponseDto<T>> GetUserProfileAsync<T>(string userId);
        Task<MicroserviceResponseDto<T>> LogoutAsync<T>();
    }
}
