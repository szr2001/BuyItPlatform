using BuyItPlatform.UserRatingApi.Models.Dto;

namespace BuyItPlatform.UserRatingApi.Services.IServices
{
    public interface IUserRatingService
    {
        Task RateUserAsync(UserRatingRequestDto ratingRequest);
        Task<UserRatingResponseDto> GetUserRatingAsync(string targetUserId);
        Task DeleteOfferedRatingsAsync(string userId);
        Task<UserRatingResponseDto[]> GetUsersScoreboardAsync(int count, int offset);
    }
}
