using BuyItPlatform.ListingsApi.Models;
using BuyItPlatform.ListingsApi.Models.Dto;

namespace BuyItPlatform.ListingsApi.Services.IServices
{
    public interface IListingsService
    {
        Task DeleteListingAsync(string userId, string listingId);
        Task DeleteUserListingsAsync(string userId);
        Task<ICollection<Listing>> GetListingsAsync(ListingFIlterDto listFilter, int count, int offset);
        Task<Listing> GetListingWithIdAsync(string listingId);
        Task UploadListingAsync(ListingUploadDto listingDto);
        Task<ICollection<Listing>> GetUserListings(string userId);
        Task<int> CountListingsAsync(ListingFIlterDto listFilter);
        Task IsListingIdPresentAsync(string listingId);
    }
}