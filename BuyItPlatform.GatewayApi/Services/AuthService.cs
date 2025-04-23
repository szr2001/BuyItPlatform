using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Services.IServices;
using BuyItPlatform.GatewayApi.Utility;

namespace BuyItPlatform.GatewayApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApiCallsService apiCallsService;
        private readonly MicroservicesUrls microservicesUrl;
        public AuthService(IApiCallsService serviceBase, MicroservicesUrls microservicesUrl)
        {
            this.apiCallsService = serviceBase;
            this.microservicesUrl = microservicesUrl;
        }

        public async Task<ResponseDto> AssignRole(string email, string rolename)
        {
            return await apiCallsService.SendAsync(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/assignRole?email={email}&roleName={rolename}"
            });
        }

        public async Task<ResponseDto> LoginUser(LoginRequestDto loginData)
        {
            return await apiCallsService.SendAsync(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Data = loginData,
                Url = $"{microservicesUrl.AuthApiUrl}/login"
            });
        }

        public async Task<ResponseDto> RegisterUser(RegisterRequestDto registerData)
        {
            return await apiCallsService.SendAsync(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Data = registerData,
                Url = $"{microservicesUrl.AuthApiUrl}/register"
            });
        }
    }
}
