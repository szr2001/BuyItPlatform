using AutoMapper;
using BuyItPlatform.AuthApi.Data;
using BuyItPlatform.AuthApi.Models;
using BuyItPlatform.AuthApi.Models.Dto;
using BuyItPlatform.AuthApi.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BuyItPlatform.AuthApi.Service
{
    public class AuthService : IAuthService
    {
        public readonly IMapper mapper;
        public readonly IJwtTokenHandler jwtTokenHandler;
        public readonly AppDbContext appContext;
        public readonly UserManager<BuyItUser> userManager;
        public readonly RoleManager<BuyItRole> roleManager;
        public AuthService(AppDbContext appContext, IMapper mapper, UserManager<BuyItUser> userManager, RoleManager<BuyItRole> roleManager, IJwtTokenHandler jwtTokenGenerator)
        {
            this.mapper = mapper;
            this.appContext = appContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtTokenHandler = jwtTokenGenerator;
        }

        public async Task RegisterUser(RegisterRequestDto registerData)
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

            await AssignRole(newUser.Email, RolesDefaults.User);
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
            var roles = await userManager.GetRolesAsync(user);
            string token = jwtTokenHandler.GenerateToken(user, roles);
            string refreshToken = jwtTokenHandler.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await userManager.UpdateAsync(user);

            LoginResponseDto loginResponseDto = new LoginResponseDto() 
            {
                User = mapper.Map<UserDto>(user),
                Token = token,
                RefreshToken = refreshToken
            };

            return loginResponseDto;
        }

        public async Task<LoginResponseDto> RefreshToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken)) throw new ArgumentNullException("refreshToken is null");

            var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            //if the user doesn't exist, or the refresh token is not the same as the one in the db 
            //or if the refreshToken expired, return, user needs to re-authentificate using pass and email
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new Exception("user is null or refresh token is expired");
            }

            var roles = await userManager.GetRolesAsync(user);
            //generate the token and the refresh token again, save the refresh token in the db
            string token = jwtTokenHandler.GenerateToken(user, roles);

            await userManager.UpdateAsync(user);

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = mapper.Map<UserDto>(user),
                Token = token,
                RefreshToken = refreshToken
            };

            return loginResponseDto;
        }

        public async Task AssignRole(string email, string rolename)
        {
            BuyItUser user = await userManager.FindByEmailAsync(email);
            if(user == null )
            {
                throw new Exception("AsignRole failed");
            }

            if(!await roleManager.RoleExistsAsync(rolename))
            {
                await roleManager.CreateAsync(new BuyItRole() { Name = rolename});
            }
            await userManager.AddToRoleAsync(user, rolename);
        }

        public async Task<UserProfileDto> GetUserProfile(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user == null) throw new Exception("user could not be found");

            UserProfileDto userProfile = mapper.Map<UserProfileDto>(user);

            return userProfile;
        }

        public async Task Logout(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken)) throw new ArgumentNullException("refreshToken is null");

            var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new Exception("user is null or refresh token is expired");
            }

            user.RefreshTokenExpiryTime = DateTime.UtcNow;

            await userManager.UpdateAsync(user);
        }
    }
}
