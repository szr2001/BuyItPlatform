using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.AuthApiDto;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IAuthService
    {
        Task<MicroserviceResponseDto<LoginResponseDto>> LoginUserAsync(LoginRequestDto registerData);
        Task<MicroserviceResponseDto<object>> RegisterUserAsync(RegisterRequestDto registerData);
        Task<MicroserviceResponseDto<object>> AssignRoleAsync(string email, string rolename);
        Task<MicroserviceResponseDto<LoginResponseDto>> RefreshTokenAsync();
        Task<MicroserviceResponseDto<UserProfileDto>> GetUserProfileAsync(string userId);
        Task<MicroserviceResponseDto<object>> LogoutAsync();
        Task<MicroserviceResponseDto<object>> IsUserIdPresent(string targetUserId);
        Task<MicroserviceResponseDto<object>> AreUserIdsPresent(string[] userIds);
    }
}
