using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.ListingApiDto;
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

        public async Task<MicroserviceResponseDto<int>> CountListingsAsync(ListingFIlterDto listFilter)
        {
            return await apiCallsService.SendAsync<int>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.ListingsApiUrl}/countListings",
                BodyData = listFilter
            });
        }

        public async Task<MicroserviceResponseDto<object>> DeleteListingAsync(int listingId)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto() 
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/deleteListing/{listingId}"
            });
        }

        public async Task<MicroserviceResponseDto<object>> DeleteUserListingsAsync()
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/deleteUserListings"
            });
        }

        public async Task<MicroserviceResponseDto<List<ListingViewDto>>> GetListingsAsync(ListingFIlterDto listFilter, int count, int offset)
        {
            return await apiCallsService.SendAsync<List<ListingViewDto>>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.ListingsApiUrl}/getListings?count={count}&offset={offset}",
                BodyData = listFilter
            });
        }

        public async Task<MicroserviceResponseDto<ListingViewDto>> GetListingWithIdAsync(int id)
        {
            return await apiCallsService.SendAsync<ListingViewDto>(new RequestDto()
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/getListingWithId/{id}"
            });
        }

        public async Task<MicroserviceResponseDto<ICollection<ListingViewDto>>> GetUserListings(string userId)
        {
            return await apiCallsService.SendAsync<ICollection<ListingViewDto>>(new RequestDto()
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/getUserListings/{userId}"
            });
        }

        public async Task<MicroserviceResponseDto<object>> UploadListingAsync(ListingUploadDto listingDto)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.ListingsApiUrl}/uploadListing",
                FormData = listingDto
            });
        }
    }
}
