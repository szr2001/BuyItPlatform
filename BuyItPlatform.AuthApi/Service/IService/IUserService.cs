using BuyItPlatform.AuthApi.Models.Dto;

namespace BuyItPlatform.AuthApi.Service.IService
{
    public interface IUserService
    {
        Task<UserProfileDto> GetUserProfile(string userId);
    }
}
