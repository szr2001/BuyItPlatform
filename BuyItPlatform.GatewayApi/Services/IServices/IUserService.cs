using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;

namespace BuyItPlatform.GatewayApi.Service.IService
{
    public interface IUserService
    {
        Task<MicroserviceResponseDto<T>> GetUserProfileAsync<T>(string userId);
        Task<MicroserviceResponseDto<T>> GetUsersProfilesAsync<T>(string[] userIds);
        Task<MicroserviceResponseDto<T>> SetUserDescAsync<T>(string desc);
        Task<MicroserviceResponseDto<T>> SetUserNameAsync<T>(string name);
        Task<MicroserviceResponseDto<T>> SetUserPhoneNumberAsync<T>(string phoneNumber);
        Task<MicroserviceResponseDto<T>> SetUserProfilePicsAsync<T>(ImageDto profilePic);
    }
}
