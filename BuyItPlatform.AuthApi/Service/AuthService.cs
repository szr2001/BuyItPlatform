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
        public readonly IJwtTokenGenerator jwtTokenGenerator;
        public readonly AppDbContext appContext;
        public readonly UserManager<BuyItUser> userManager;
        public readonly RoleManager<BuyItRole> roleManager;
        public AuthService(AppDbContext appContext, IMapper mapper, UserManager<BuyItUser> userManager, RoleManager<BuyItRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.mapper = mapper;
            this.appContext = appContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<UserDto> RegisterUser(RegisterRequestDto registerData)
        {
            if(registerData.Password != registerData.RepeatPassword)
            {
                throw new Exception("Passwords doesn't match.");
            }

            BuyItUser newUser = new()
            {
                UserName = registerData.Name,
                Email = registerData.Email
            };
            var result = await userManager.CreateAsync(newUser, registerData.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(",", result.Errors.Select(i=> i.Description)));
            }

            BuyItUser returnedUser = await userManager.FindByEmailAsync(registerData.Email);

            UserDto userDto = mapper.Map<UserDto>(returnedUser);

            return userDto;
        }

        public async Task<LoginResponseDto> LoginUser(LoginRequestDto registerData)
        {
            BuyItUser user = await userManager.FindByEmailAsync(registerData.Email);
            if(user == null)
            {
                throw new Exception("Wrong Email or password!");
            }
            var isValid = await userManager.CheckPasswordAsync(user,registerData.Password);

            if (!isValid)
            {
                throw new Exception("Wrong Email or password!");
            }

            string token = jwtTokenGenerator.GenerateToken(user);

            LoginResponseDto loginResponseDto = new LoginResponseDto() 
            {
                User = mapper.Map<UserDto>(user),
                Token = token,
            };

            return loginResponseDto;
        }
    }
}
