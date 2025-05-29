using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Services.IServices;
using BuyItPlatform.GatewayApi.Utility;

namespace BuyItPlatform.GatewayApi.Services
{
    public class UserRatingService : IUserRatingService
    {
        private readonly IApiCallsService apiCallsService;
        private readonly MicroservicesUrls microservicesUrl;
        public UserRatingService(IApiCallsService serviceBase, MicroservicesUrls microservicesUrl)
        {
            this.apiCallsService = serviceBase;
            this.microservicesUrl = microservicesUrl;
        }

        public async Task<MicroserviceResponseDto<T>> GetUsersScoreboardAsync<T>(int count, int offset)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.UserRatingApiUrl}/getUsersScoreboard/{count}/{offset}"
            });
        }

        public async Task<MicroserviceResponseDto<T>> DeleteOfferedRatingsAsync<T>(string userId)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.UserRatingApiUrl}/deleteOfferedRatings/{userId}"
            });
        }

        public async Task<MicroserviceResponseDto<T>> GetUserRatingAsync<T>(string targetUserId)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.UserRatingApiUrl}/getUserRating/{targetUserId}"
            });
        }

        public async Task<MicroserviceResponseDto<T>> RateUserAsync<T>(UserRatingRequestDto ratingRequest)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                BodyData = ratingRequest,
                Url = $"{microservicesUrl.UserRatingApiUrl}/rateUser"
            });
        }
    }
}
