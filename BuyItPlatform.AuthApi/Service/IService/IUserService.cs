using BuyItPlatform.AuthApi.Models.Dto;

namespace BuyItPlatform.AuthApi.Service.IService
{
    public interface IUserService
    {
        Task<UserProfileDto> GetUserProfileAsync(string userId);
        Task<UserProfileDto[]> GetUsersProfilesAsync(string[] userId);
        Task SetUserNameAsync(string userId, string name);
        Task SetUserDescAsync(string userId, string desc);
        Task<string> SetUserProfilePicAsync(string userId, ImageDto profilePic);
        Task SetUserPhoneNumberAsync(string userId, string phoneNumber);
        Task AreUserIdsPresentAsync(string[] userIds);
        Task IsUserIdPresentAsync(string userId);
    }
}
