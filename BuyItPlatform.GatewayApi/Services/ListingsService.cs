using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Services.IServices;

namespace BuyItPlatform.GatewayApi.Services
{
    public class ListingsService : IListingsService
    {
        private readonly IApiCallsService serviceBase;

        public ListingsService(IApiCallsService serviceBase)
        {
            this.serviceBase = serviceBase;
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
