using BuyItPlatform.AuthApi.Models.Dto;

namespace BuyItPlatform.AuthApi.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginUser(LoginRequestDto registerData);
        Task RegisterUser(RegisterRequestDto registerData);
        Task AssignRole(string email, string rolename);
        Task<LoginResponseDto> RefreshToken(string refreshToken);
        Task Logout(string? refreshToken);
    }
}
