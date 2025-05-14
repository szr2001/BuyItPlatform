using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Service.IService;
using BuyItPlatform.GatewayApi.Services;
using BuyItPlatform.GatewayApi.Services.IServices;
using BuyItPlatform.GatewayApi.Utility;
using Microsoft.AspNetCore.Identity;

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

        public async Task<ResponseDto<T>> GetUserProfileAsync<T>(string userId)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.AuthApiUrl}/user/getUserProfile/{userId}"
            });
        }
        public async Task<ResponseDto<T>> UpdateUserDescAsync<T>(string desc)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST    ,
                Url = $"{microservicesUrl.AuthApiUrl}/user/updateUserDesc/{desc}"
            });
        }

        public async Task<ResponseDto<T>> UpdateUserNameAsync<T>(string name)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/user/updateUserName/{name}"
            });
        }

        public async Task<ResponseDto<T>> UpdateUserPhoneNumberAsync<T>(string phoneNumber)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/user/updateUserPhoneNumber/{phoneNumber}"
            });
        }

        public async Task<ResponseDto<T>> UpdateUserProfilePicsAsync<T>(IFormFile profilePic)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Data = profilePic,
                Url = $"{microservicesUrl.AuthApiUrl}/user/updateUserProfilePic"
            });
        }
    }
}
