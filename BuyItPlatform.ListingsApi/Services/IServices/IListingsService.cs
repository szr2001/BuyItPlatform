using BuyItPlatform.ListingsApi.Models;
using BuyItPlatform.ListingsApi.Models.Dto;

namespace BuyItPlatform.ListingsApi.Services.IServices
{
    public interface IListingsService
    {
        Task DeleteListingAsync(int listingId);
        Task DeleteUserListingsAsync(int userId);
        Task<List<Listing>> GetListingsAsync(ListingFIlterDto listFilter, int count, int offset);
        Task<Listing> GetListingWithIdAsync(int id);
        Task UploadListingAsync(ListingDto listingDto);
    }
}