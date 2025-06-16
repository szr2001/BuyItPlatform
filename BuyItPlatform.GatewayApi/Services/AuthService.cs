using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.AuthApiDto;
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

        public async Task<MicroserviceResponseDto<object>> AssignRoleAsync(string email, string rolename)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/assignRole?email={email}&roleName={rolename}"
            });
        }

        public async Task<MicroserviceResponseDto<UserProfileDto>> GetUserProfileAsync(string userId)
        {
            return await apiCallsService.SendAsync<UserProfileDto>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/getUserProfile/{userId}"
            });
        }

        public async Task<MicroserviceResponseDto<LoginResponseDto>> LoginUserAsync(LoginRequestDto loginData)
        {
            return await apiCallsService.SendAsync<LoginResponseDto>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                BodyData = loginData,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/login"
            }, false);
        }

        public async Task<MicroserviceResponseDto<object>> LogoutAsync()
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/logout"
            });
        }

        public async Task<MicroserviceResponseDto<LoginResponseDto>> RefreshTokenAsync()
        {
            return await apiCallsService.SendAsync<LoginResponseDto>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/refreshToken"
            });
        }
        public async Task<MicroserviceResponseDto<object>> IsUserIdPresent(string targetUserId)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/user/isUserIdPresent/{targetUserId}"
            });
        }
        public async Task<MicroserviceResponseDto<object>> AreUserIdsPresent(string[] userIds)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                BodyData = userIds,
                Url = $"{microservicesUrl.AuthApiUrl}/user/areUserIdsPresent"
            });
        }
        public async Task<MicroserviceResponseDto<object>> RegisterUserAsync(RegisterRequestDto registerData)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                BodyData = registerData,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/register"
            }, false);
        }
    }
}
