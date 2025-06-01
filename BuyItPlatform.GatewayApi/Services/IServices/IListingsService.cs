using BuyItPlatform.GatewayApi.Models;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IListingsService
    {
        Task<MicroserviceResponseDto<object>> DeleteListingAsync(int listingId);
        Task<MicroserviceResponseDto<object>> DeleteUserListingsAsync();
        Task<MicroserviceResponseDto<List<ListingDto>>> GetListingsAsync(ListingFIlterDto listFilter, int count, int offset);
        Task<MicroserviceResponseDto<ListingDto>> GetListingWithIdAsync(int id);
        Task<MicroserviceResponseDto<object>> UploadListingAsync(ListingDto listingDto);
        Task<MicroserviceResponseDto<ICollection<ListingDto>>> GetUserListings(string userId);
    }
}
