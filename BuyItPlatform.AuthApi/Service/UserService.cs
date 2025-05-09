using AutoMapper;
using BuyItPlatform.AuthApi.Data;
using BuyItPlatform.AuthApi.Models;
using BuyItPlatform.AuthApi.Models.Dto;
using BuyItPlatform.AuthApi.Service.IService;
using BuyItPlatform.AuthApi.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace BuyItPlatform.AuthApi.Service
{
    public class UserService : IUserService
    {
        public readonly IMapper mapper;
        public readonly IJwtTokenHandler jwtTokenHandler;
        private readonly IImageUploader imageUploader;
        public readonly UserManager<BuyItUser> userManager;

        public UserService(IMapper mapper, IJwtTokenHandler jwtTokenHandler, UserManager<BuyItUser> userManager, IImageUploader imageUploader)
        {
            this.mapper = mapper;
            this.jwtTokenHandler = jwtTokenHandler;
            this.userManager = userManager;
            this.imageUploader = imageUploader;
        }

        public async Task<UserProfileDto> GetUserProfile(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("user could not be found");
            }

            UserProfileDto userProfile = mapper.Map<UserProfileDto>(user);

            return userProfile;
        }

        public async Task UpdateUserDesc(string userId, string desc)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Refresh token is missing or expired");
            }

            var user = await userManager.FindByIdAsync(userId);

            //if the user doesn't exist, or the refresh token is not the same as the one in the db 
            //or if the refreshToken expired, return, user needs to re-authentificate using pass and email
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new Exception("Refresh token is missing or expired");
            }

            user.Description = desc;
            await userManager.UpdateAsync(user);
        }

        public async Task UpdateUserName(string userId, string name)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserPhoneNumber(string userId, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserProfilePic(string userId, IFormFile profilePic)
        {
            throw new NotImplementedException();
        }
    }
}
