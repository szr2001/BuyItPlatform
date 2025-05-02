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

            await AssignRole(newUser.Email, RolesDefaults.User);

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

        public async Task<LoginResponseDto?> RefreshToken(RefreshTokenRequest request)
        {
            var principal = jwtTokenHandler.GetTokenPrincipal(request.Token);
            if(principal?.Identity?.Name == null) return null;

            var user = await userManager.FindByEmailAsync(principal.Identity.Name);

            if (user == null || user.RefreshToken != request.RefreshToken 
                || user.RefreshTokenExpiryTime > DateTime.UtcNow) return null;

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
    }
}
