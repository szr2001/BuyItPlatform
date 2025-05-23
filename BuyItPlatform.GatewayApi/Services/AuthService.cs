﻿using BuyItPlatform.GatewayApi.Models;
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

        public async Task<MicroserviceResponseDto<T>> AssignRole<T>(string email, string rolename)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/assignRole?email={email}&roleName={rolename}"
            });
        }

        public async Task<MicroserviceResponseDto<T>> GetUserProfile<T>(string userId)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/getUserProfile/{userId}"
            });
        }

        public async Task<MicroserviceResponseDto<T>> LoginUser<T>(LoginRequestDto loginData)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                BodyData = loginData,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/login"
            },false);
        }

        public async Task<MicroserviceResponseDto<T>> Logout<T>()
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/logout"
            });
        }

        public async Task<MicroserviceResponseDto<T>> RefreshToken<T>()
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/refreshToken"
            });
        }

        public async Task<MicroserviceResponseDto<T>> RegisterUser<T>(RegisterRequestDto registerData)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                BodyData = registerData,
                Url = $"{microservicesUrl.AuthApiUrl}/auth/register"
            }, false);
        }
    }
}
