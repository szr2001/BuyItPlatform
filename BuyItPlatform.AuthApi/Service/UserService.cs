using AutoMapper;
using BuyItPlatform.AuthApi.Data;
using BuyItPlatform.AuthApi.Models;
using BuyItPlatform.AuthApi.Models.Dto;
using BuyItPlatform.AuthApi.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace BuyItPlatform.AuthApi.Service
{
    public class UserService : IUserService
    {
        public readonly IMapper mapper;
        public readonly IJwtTokenHandler jwtTokenHandler;
        public readonly UserManager<BuyItUser> userManager;

        public UserService(IMapper mapper, IJwtTokenHandler jwtTokenHandler, UserManager<BuyItUser> userManager)
        {
            this.mapper = mapper;
            this.jwtTokenHandler = jwtTokenHandler;
            this.userManager = userManager;
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
    }
}
