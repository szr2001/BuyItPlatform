using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Service.IService;
using BuyItPlatform.GatewayApi.Services.IServices;
using BuyItPlatform.GatewayApi.Utility;

namespace BuyItPlatform.GatewayApi.Service
{
    public class UserService : IUserService
    {
        private readonly IApiCallsService apiCallsService;
        private readonly MicroservicesUrls microservicesUrl;

        public UserService(IApiCallsService serviceBase, MicroservicesUrls microservicesUrl)
        {
            this.apiCallsService = serviceBase;
            this.microservicesUrl = microservicesUrl;
        }

        public async Task<MicroserviceResponseDto<UserProfileDto>> GetUserProfileAsync(string userId)
        {
            return await apiCallsService.SendAsync<UserProfileDto>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.AuthApiUrl}/user/getUserProfile/{userId}"
            });
        }
        
        public async Task<MicroserviceResponseDto<UserProfileDto[]>> GetUsersProfilesAsync(string[] userIds)
        {
            return await apiCallsService.SendAsync<UserProfileDto[]>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                BodyData = userIds,
                Url = $"{microservicesUrl.AuthApiUrl}/user/getUsersProfiles"
            });
        }

        public async Task<MicroserviceResponseDto<object>> SetUserDescAsync(string desc)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/user/updateUserDesc/{desc}"
            });
        }

        public async Task<MicroserviceResponseDto<object>> SetUserNameAsync(string name)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/user/updateUserName/{name}"
            });
        }

        public async Task<MicroserviceResponseDto<object>> SetUserPhoneNumberAsync(string phoneNumber)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/user/updateUserPhoneNumber/{phoneNumber}"
            });
        }

        public async Task<MicroserviceResponseDto<string>> SetUserProfilePicAsync(ImageDto profilePic)
        {
            return await apiCallsService.SendAsync<string>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                FormData = profilePic,
                Url = $"{microservicesUrl.AuthApiUrl}/user/updateUserProfilePic"
            });
        }
    }
}
