﻿using BuyItPlatform.AuthApi.Models.Dto;

namespace BuyItPlatform.AuthApi.Service.IService
{
    public interface IUserService
    {
        Task<UserProfileDto> GetUserProfile(string userId);
        Task UpdateUserName(string userId, string name);
        Task UpdateUserDesc(string userId, string desc);
        Task UpdateUserProfilePic(string userId, IFormFile profilePic);
        Task UpdateUserPhoneNumber(string userId, string phoneNumber);
    }
}
