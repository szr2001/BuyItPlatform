using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IUserRatingService
    {
        Task<MicroserviceResponseDto<T>> RateUser<T>(UserRatingRequestDto ratingRequest);
        Task<MicroserviceResponseDto<T>> GetUserRating<T>(string targetUserId);
        Task<MicroserviceResponseDto<T>> DeleteOfferedRatings<T>(string userId);
    }
}
