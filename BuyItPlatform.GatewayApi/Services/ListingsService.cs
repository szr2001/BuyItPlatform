using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Services.IServices;
using BuyItPlatform.GatewayApi.Utility;

namespace BuyItPlatform.GatewayApi.Services
{
    public class ListingsService : IListingsService
    {
        private readonly IApiCallsService apiCallsService;
        private readonly MicroservicesUrls microservicesUrl;
        public ListingsService(IApiCallsService serviceBase, MicroservicesUrls microservicesUrl)
        {
            this.apiCallsService = serviceBase;
            this.microservicesUrl = microservicesUrl;
        }

        public async Task<ResponseDto<T>> DeleteListingAsync<T>(int listingId)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto() 
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/DeleteListing/{listingId}"
            });
        }

        public async Task<ResponseDto<T>> DeleteUserListingsAsync<T>(int userId)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/DeleteUserListings/{userId}"
            });
        }

        public async Task<ResponseDto<T>> GetListingsAsync<T>(ListingFIlterDto listFilter, int count, int offset)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.ListingsApiUrl}/GetListings?count={count}&offset={offset}",
                Data = listFilter
            });
        }

        public async Task<ResponseDto<T>> GetListingWithIdAsync<T>(int id)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/GetListingWithId/{id}"
            });
        }

        public async Task<ResponseDto<T>> UploadListingAsync<T>(ListingDto listingDto)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.ListingsApiUrl}/UploadListing",
                Data = listingDto
            });
        }
    }
}
