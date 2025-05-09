using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;

namespace BuyItPlatform.GatewayApi.Service.IService
{
    public interface IUserService
    {
        Task<ResponseDto<T>> GetUserProfileAsync<T>(string userId);
        Task<ResponseDto<T>> UpdateUserDescAsync<T>(string desc);
        Task<ResponseDto<T>> UpdateUserNameAsync<T>(string name);
        Task<ResponseDto<T>> UpdateUserPhoneNumberAsync<T>(string phoneNumber);
        Task<ResponseDto<T>> UpdateUserProfilePicsAsync<T>(IFormFile profilePic);
    }
}
