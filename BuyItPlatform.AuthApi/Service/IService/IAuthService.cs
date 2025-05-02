using BuyItPlatform.AuthApi.Models.Dto;

namespace BuyItPlatform.AuthApi.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginUser(LoginRequestDto registerData);
        Task<UserDto> RegisterUser(RegisterRequestDto registerData);
        Task AssignRole(string email, string rolename);
        Task<LoginResponseDto?> RefreshToken(RefreshTokenRequest request);
    }
}
