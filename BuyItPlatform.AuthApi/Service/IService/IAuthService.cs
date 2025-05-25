using BuyItPlatform.AuthApi.Models.Dto;

namespace BuyItPlatform.AuthApi.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginUserAsync(LoginRequestDto registerData);
        Task RegisterUserAsync(RegisterRequestDto registerData);
        Task AssignRoleAsync(string email, string rolename);
        Task<LoginResponseDto> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync(string? refreshToken);
    }
}
