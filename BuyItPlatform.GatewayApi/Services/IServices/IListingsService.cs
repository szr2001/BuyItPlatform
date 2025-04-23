using BuyItPlatform.GatewayApi.Models;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IListingsService
    {
        Task DeleteListingAsync(int listingId);
        Task DeleteUserListingsAsync(int userId);
        Task<List<ListingDto>> GetListingsAsync(ListingFIlterDto listFilter, int count, int offset);
        Task<ListingDto> GetListingWithIdAsync(int id);
        Task UploadListingAsync(ListingDto listingDto);
    }
}
