using BuyItPlatform.GatewayApi.Models;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginUser(LoginRequestDto registerData);
        Task<ResponseDto> RegisterUser(RegisterRequestDto registerData);
        Task<ResponseDto> AssignRole(string email, string rolename);
    }
}
