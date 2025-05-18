using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IUserRatingService
    {
        Task<ResponseDto<T>> RateUser<T>(UserRatingRequestDto ratingRequest);
        Task<ResponseDto<T>> GetUserRating<T>(string targetUserId);
        Task<ResponseDto<T>> DeleteOfferedRatings<T>(string userId);
    }
}
