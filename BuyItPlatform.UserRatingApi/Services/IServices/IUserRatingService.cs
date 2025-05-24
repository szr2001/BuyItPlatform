using BuyItPlatform.UserRatingApi.Models.Dto;

namespace BuyItPlatform.UserRatingApi.Services.IServices
{
    public interface IUserRatingService
    {
        Task RateUser(UserRatingRequestDto ratingRequest);
        Task<UserRatingResponseDto> GetUserRating(string targetUserId);
        Task DeleteOfferedRatings(string userId);
        Task<string[]> GetUsersScoreboard(int count, int offset);
    }
}
