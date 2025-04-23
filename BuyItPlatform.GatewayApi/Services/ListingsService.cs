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

        public Task DeleteListingAsync(int listingId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserListingsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ListingDto>> GetListingsAsync(ListingFIlterDto listFilter, int count, int offset)
        {
            throw new NotImplementedException();
        }

        public Task<ListingDto> GetListingWithIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UploadListingAsync(ListingDto listingDto)
        {
            throw new NotImplementedException();
        }
    }
}
