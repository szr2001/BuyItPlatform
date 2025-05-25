using AutoMapper;
using BuyItPlatform.AuthApi.Data;
using BuyItPlatform.AuthApi.Models;
using BuyItPlatform.AuthApi.Models.Dto;
using BuyItPlatform.AuthApi.Service.IService;
using BuyItPlatform.AuthApi.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Xml.Linq;

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

        public async Task<UserProfileDto> GetUserProfileAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("user could not be found");
            }

            UserProfileDto userProfile = mapper.Map<UserProfileDto>(user);

            return userProfile;
        }

        public async Task<UserProfileDto[]> GetUsersProfilesAsync(string[] userId)
        {
            var userProfiles = await userManager.Users.Where(u => userId.Contains(u.Id)).ToArrayAsync();
            var userProfilesDto = mapper.Map<UserProfileDto[]>(userProfiles);

            return userProfilesDto;
        }

        public async Task SetUserDescAsync(string userId, string desc)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new KeyNotFoundException("user could not be found");
            }

            var user = await userManager.FindByIdAsync(userId);

            //if the user doesn't exist, or the refresh token is not the same as the one in the db 
            //or if the refreshToken expired, return, user needs to re-authentificate using pass and email
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token is missing or expired");
            }

            user.Description = desc;
            await userManager.UpdateAsync(user);
        }

        public async Task SetUserNameAsync(string userId, string name)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new KeyNotFoundException("User could not be found");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new KeyNotFoundException("Name is empty");
            }

            var user = await userManager.FindByIdAsync(userId);

            //if the user doesn't exist, or the refresh token is not the same as the one in the db 
            //or if the refreshToken expired, return, user needs to re-authentificate using pass and email
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token is missing or expired");
            }

            user.UserName = name;
            user.NormalizedUserName = name.ToUpper();
            await userManager.UpdateAsync(user);
        }

        public async Task SetUserPhoneNumberAsync(string userId, string phoneNumber)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new KeyNotFoundException("User could not be found");
            }
            if (string.IsNullOrEmpty(phoneNumber) && phoneNumber.Length > 15 && phoneNumber.Length < 8)
            {
                throw new KeyNotFoundException("Phone number must be between 8-15 numbers");
            }

            var user = await userManager.FindByIdAsync(userId);

            //if the user doesn't exist, or the refresh token is not the same as the one in the db 
            //or if the refreshToken expired, return, user needs to re-authentificate using pass and email
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token is missing or expired");
            }

            user.PhoneNumber = phoneNumber;
            await userManager.UpdateAsync(user);
        }

        public async Task<string> SetUserProfilePicAsync(string userId, ImageDto profilePic)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new KeyNotFoundException("User could not be found");
            }

            var user = await userManager.FindByIdAsync(userId);

            //if the user doesn't exist, or the refresh token is not the same as the one in the db 
            //or if the refreshToken expired, return, user needs to re-authentificate using pass and email
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token is missing or expired");
            }

            if(user.ProfileImgLink != " ")
            {
                await imageUploader.DeleteImagesAsync(user.Id);
            }

            var imageUpload = await imageUploader.UploadImagesAsync(userId, [profilePic.ImgFile]);

            user.ProfileImgLink = imageUpload[0];
            await userManager.UpdateAsync(user);
            return user.ProfileImgLink;
        }

        public async Task AreUserIdsPresentAsync(string[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
            {
                throw new IndexOutOfRangeException("userIds is empty");
            }

            var validUserIds = await userManager.Users
                .Where(u => userIds.Contains(u.Id))
                .Select(u => u.Id)
                .ToListAsync();

            var missingIds = userIds.Except(validUserIds).ToList();

            if (missingIds.Any())
            {
                throw new KeyNotFoundException($"UserIds are not correct or not valid");
            }
        }

        public async Task IsUserIdPresentAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new IndexOutOfRangeException("userId empty");
            }

            if ((await userManager.FindByIdAsync(userId)) == null)
            {
                throw new KeyNotFoundException("UserId is not correct or not valid");
            }
        }
    }
}
