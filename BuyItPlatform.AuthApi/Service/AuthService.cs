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
        private readonly IMapper mapper;
        private readonly IJwtTokenHandler jwtTokenHandler;
        private readonly UserManager<BuyItUser> userManager;
        private readonly RoleManager<BuyItRole> roleManager;
        public AuthService(IMapper mapper, UserManager<BuyItUser> userManager, RoleManager<BuyItRole> roleManager, IJwtTokenHandler jwtTokenGenerator)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtTokenHandler = jwtTokenGenerator;
        }

        public async Task RegisterUserAsync(RegisterRequestDto registerData)
        {
            // SAVE TO CACHE //

            if (registerData.Password != registerData.RepeatPassword)
            {
                throw new ArgumentException("Passwords doesn't match.");
            }

            BuyItUser newUser = new()
            {
                UserName = registerData.Name,
                Email = registerData.Email,
                PhoneNumber = " "
            };
            var result = await userManager.CreateAsync(newUser, registerData.Password);
            if (!result.Succeeded)
            {
                throw new ArgumentException(string.Join(",", result.Errors.Select(i=> i.Description)));
            }

            await AssignRoleAsync(newUser.Email, RolesDefaults.User);
        }

        public async Task<LoginResponseDto> LoginUserAsync(LoginRequestDto registerData)
        {
            BuyItUser user = await userManager.FindByEmailAsync(registerData.Email);
            if(user == null)
            {
                throw new ArgumentException("Wrong Email or password!");
            }
            var isValid = await userManager.CheckPasswordAsync(user,registerData.Password);

            if (!isValid)
            {
                throw new ArgumentException("Wrong Email or password!");
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

        public async Task<LoginResponseDto> RefreshTokenAsync(string refreshToken)
        {
            // SAVE TO CACHE //

            if (string.IsNullOrEmpty(refreshToken))
            { 
                throw new UnauthorizedAccessException("Refresh token is missing or expired"); 
            }

            var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            //if the user doesn't exist, or the refresh token is not the same as the one in the db 
            //or if the refreshToken expired, return, user needs to re-authentificate using pass and email
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token is missing or expired");
            }

            var roles = await userManager.GetRolesAsync(user);
            //generate the token 
            //we don't use refresh token rotation because I am lazy
            //and I won't host the project anyway
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

        public async Task AssignRoleAsync(string email, string rolename)
        {
            BuyItUser user = await userManager.FindByEmailAsync(email);
            if(user == null )
            {
                throw new ArgumentException("AsignRole failed");
            }

            if(!await roleManager.RoleExistsAsync(rolename))
            {
                await roleManager.CreateAsync(new BuyItRole() { Name = rolename});
            }
            await userManager.AddToRoleAsync(user, rolename);
        }

        public async Task LogoutAsync(string? refreshToken)
        {
            // SAVE TO CACHE //

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new UnauthorizedAccessException("Refresh token is missing or expired");
            }

            var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token is missing or expired");
            }

            user.RefreshTokenExpiryTime = DateTime.UtcNow;

            await userManager.UpdateAsync(user);
        }
    }
}
