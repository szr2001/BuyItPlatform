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

        public async Task<MicroserviceResponseDto<T>> DeleteListingAsync<T>(int listingId)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto() 
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/deleteListing/{listingId}"
            });
        }

        public async Task<MicroserviceResponseDto<T>> DeleteUserListingsAsync<T>(int userId)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/deleteUserListings/{userId}"
            });
        }

        public async Task<MicroserviceResponseDto<T>> GetListingsAsync<T>(ListingFIlterDto listFilter, int count, int offset)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.ListingsApiUrl}/getListings?count={count}&offset={offset}",
                BodyData = listFilter
            });
        }

        public async Task<MicroserviceResponseDto<T>> GetListingWithIdAsync<T>(int id)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/getListingWithId/{id}"
            });
        }

        public async Task<MicroserviceResponseDto<T>> UploadListingAsync<T>(ListingDto listingDto)
        {
            return await apiCallsService.SendAsync<T>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.ListingsApiUrl}/uploadListing",
                FormData = listingDto
            });
        }
    }
}
