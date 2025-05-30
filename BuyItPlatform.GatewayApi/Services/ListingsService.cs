using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Services.IServices;
using BuyItPlatform.GatewayApi.Utility;
using System.Collections.Generic;

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

        public async Task<MicroserviceResponseDto<object>> DeleteListingAsync(int listingId)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto() 
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/deleteListing/{listingId}"
            });
        }

        public async Task<MicroserviceResponseDto<object>> DeleteUserListingsAsync(int userId)
        {
            return await apiCallsService.SendAsync<object>(new RequestDto()
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/deleteUserListings/{userId}"
            });
        }

        public async Task<MicroserviceResponseDto<List<ListingDto>>> GetListingsAsync(ListingFIlterDto listFilter, int count, int offset)
        {
            return await apiCallsService.SendAsync<List<ListingDto>>(new RequestDto()
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{microservicesUrl.ListingsApiUrl}/getListings?count={count}&offset={offset}",
                BodyData = listFilter
            });
        }

        public async Task<MicroserviceResponseDto<ListingDto>> GetListingWithIdAsync(int id)
        {
            return await apiCallsService.SendAsync<ListingDto>(new RequestDto()
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/getListingWithId/{id}"
            });
        }

        public async Task<MicroserviceResponseDto<ICollection<ListingDto>>> GetUserListings(string userId)
        {
            return await apiCallsService.SendAsync<ICollection<ListingDto>>(new RequestDto()
            {
                Url = $"{microservicesUrl.ListingsApiUrl}/getUserListings/{userId}"
            });
        }

        public async Task<MicroserviceResponseDto<object>> UploadListingAsync(ListingDto listingDto)
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
