using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Services.IServices;
using BuyItPlatform.GatewayApi.Utility;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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

        public async Task<ResponseDto<T>> AssignRole<T>(string email, string rolename)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/assignRole?email={email}&roleName={rolename}"
            });
        }

        public async Task<ResponseDto<T>> GetUserProfile<T>(string userId)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/GetUserProfile/{userId}"
            });
        }

        public async Task<ResponseDto<T>> LoginUser<T>(LoginRequestDto loginData)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Data = loginData,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/login"
            },false);
        }

        public async Task<ResponseDto<T>> Logout<T>()
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/Logout"
            });
        }

        public async Task<ResponseDto<T>> RefreshToken<T>()
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/RefreshToken"
            });
        }

        public async Task<ResponseDto<T>> RegisterUser<T>(RegisterRequestDto registerData)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Data = registerData,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/register"
            }, false);
        }
    }
}
