using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.AuthApiDto;

namespace BuyItPlatform.GatewayApi.Service.IService
{
    public interface IUserService
    {
        Task<MicroserviceResponseDto<UserProfileDto>> GetUserProfileAsync(string userId);
        Task<MicroserviceResponseDto<UserProfileDto[]>> GetUsersProfilesAsync(string[] userIds);
        Task<MicroserviceResponseDto<object>> SetUserDescAsync(string desc);
        Task<MicroserviceResponseDto<object>> SetUserNameAsync(string name);
        Task<MicroserviceResponseDto<object>> SetUserPhoneNumberAsync(string phoneNumber);
        Task<MicroserviceResponseDto<string>> SetUserProfilePicAsync(ImageDto profilePic);
    }
}
