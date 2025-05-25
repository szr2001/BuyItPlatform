using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;

namespace BuyItPlatform.GatewayApi.Service.IService
{
    public interface IUserService
    {
        Task<MicroserviceResponseDto<T>> GetUserProfileAsync<T>(string userId);
        Task<MicroserviceResponseDto<T>> GetUsersProfilesAsync<T>(string[] userIds);
        Task<MicroserviceResponseDto<T>> UpdateUserDescAsync<T>(string desc);
        Task<MicroserviceResponseDto<T>> UpdateUserNameAsync<T>(string name);
        Task<MicroserviceResponseDto<T>> UpdateUserPhoneNumberAsync<T>(string phoneNumber);
        Task<MicroserviceResponseDto<T>> UpdateUserProfilePicsAsync<T>(ImageDto profilePic);
    }
}
