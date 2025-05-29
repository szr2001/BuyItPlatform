using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IUserRatingService
    {
        Task<MicroserviceResponseDto<T>> RateUserAsync<T>(UserRatingRequestDto ratingRequest);
        Task<MicroserviceResponseDto<T>> GetUserRatingAsync<T>(string targetUserId);
        Task<MicroserviceResponseDto<T>> DeleteOfferedRatingsAsync<T>(string userId);
        Task<MicroserviceResponseDto<T>> GetUsersScoreboardAsync<T>(int count, int offset);
    }
}
