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

        public async Task<MicroserviceResponseDto<UserProfileDto[]>> GetUsersScoreboard(int count, int offset)
        {
            var scoreboardResult = await apiCallsService.SendAsync<UserRatingResponseDto[]>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.UserRatingApiUrl}/getUsersScoreboard/{count}/{offset}"
            });

            if (!scoreboardResult.Success || scoreboardResult.Result == null)
            {
                throw new Exception(scoreboardResult.Message);
            }

            var userIds = scoreboardResult.Result.Select(u => u.TargetUserId).ToArray();
            var apiResult = await apiCallsService.SendAsync<UserProfileDto[]>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                BodyData = userIds,
                Url = $"{microservicesUrl.AuthApiUrl}/user/getUsersProfiles"
            });

            if (!apiResult.Success || apiResult.Result == null)
            {
                throw new Exception(apiResult.Message);
            }

            //create a dictionary for lookups
            var ratingLookup = scoreboardResult.Result.ToDictionary(
                x => x.TargetUserId, //key
                x => new { x.AverageRating, x.NumberOfRatings }); //value

            //loop through the userProfileDto's
            foreach (var profile in apiResult.Result)
            {
                //check the profile id against the lookup table containing the data from the
                //userRating api and if the id match we set the userProfile rating data
                if (ratingLookup.TryGetValue(profile.Id, out var ratingInfo))
                {
                    profile.AverageRating = ratingInfo.AverageRating;
                    profile.NumberOfRatings = ratingInfo.NumberOfRatings;
                }
            }
            return apiResult;
        }

        public async Task<MicroserviceResponseDto<T>> DeleteOfferedRatings<T>(string userId)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.UserRatingApiUrl}/deleteOfferedRatings/{userId}"
            });
        }

        public async Task<MicroserviceResponseDto<T>> GetUserRating<T>(string targetUserId)
        {
            var userIds = await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/user/isUserIdPresent/{targetUserId}"
            });

            if (!userIds.Success)
            {
                return userIds;
            }

            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{microservicesUrl.UserRatingApiUrl}/getUserRating/{targetUserId}"
            });
        }

        public async Task<MicroserviceResponseDto<T>> RateUser<T>(UserRatingRequestDto ratingRequest)
        {

            var userIds = await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.AuthApiUrl}/user/isUserIdPresent/{ratingRequest.TargetUserId}"
            });

            if (!userIds.Success)
            {
                return userIds;
            }

            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                BodyData = ratingRequest,
                Url = $"{microservicesUrl.UserRatingApiUrl}/rateUser"
            });
        }
    }
}
