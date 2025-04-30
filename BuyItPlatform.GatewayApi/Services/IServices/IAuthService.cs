using BuyItPlatform.GatewayApi.Models;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto<T>> LoginUser<T>(LoginRequestDto registerData);
        Task<ResponseDto<T>> RegisterUser<T>(RegisterRequestDto registerData);
        Task<ResponseDto<T>> AssignRole<T>(string email, string rolename);
    }
}
