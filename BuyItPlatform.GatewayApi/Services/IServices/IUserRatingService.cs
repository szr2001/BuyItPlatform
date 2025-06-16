using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.UserRatingApiDto;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IUserRatingService
    {
        Task<MicroserviceResponseDto<object>> RateUserAsync(UserRatingRequestDto ratingRequest);
        Task<MicroserviceResponseDto<UserRatingResponseDto>> GetUserRatingAsync(string targetUserId);
        Task<MicroserviceResponseDto<object>> DeleteOfferedRatingsAsync(string userId);
        Task<MicroserviceResponseDto<UserRatingResponseDto[]>> GetUsersScoreboardAsync(int count, int offset);
    }
}
