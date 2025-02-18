using AutoMapper;
using BuyItPlatform.AuthApi.Data;
using BuyItPlatform.AuthApi.Models;
using BuyItPlatform.AuthApi.Models.Dto;
using BuyItPlatform.AuthApi.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace BuyItPlatform.AuthApi.Service
{
    public class AuthService : IAuthService
    {
        public readonly IMapper mapper;
        public readonly AppDbContext appContext;
        public readonly UserManager<BuyItUser> userManager;
        public readonly RoleManager<BuyItRole> roleManager;
        public AuthService(AppDbContext appContext, IMapper mapper, UserManager<BuyItUser> userManager, RoleManager<BuyItRole> roleManager)
        {
            this.mapper = mapper;
            this.appContext = appContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<UserDto> RegisterUser(RegisterRequestDto registerData)
        {
            return null;
        }

        public async Task<LoginResponseDto> LoginUser(LoginRequestDto registerData)
        {
            return null;
        }
    }
}
