using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.ListingApiDto;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IListingsService
    {
        Task<MicroserviceResponseDto<object>> DeleteListingAsync(int listingId);
        Task<MicroserviceResponseDto<object>> DeleteUserListingsAsync();
        Task<MicroserviceResponseDto<List<ListingViewDto>>> GetListingsAsync(ListingFIlterDto listFilter, int count, int offset);
        Task<MicroserviceResponseDto<int>> CountListingsAsync(ListingFIlterDto listFilter);
        Task<MicroserviceResponseDto<ListingViewDto>> GetListingWithIdAsync(int id);
        Task<MicroserviceResponseDto<object>> UploadListingAsync(ListingUploadDto listingDto);
        Task<MicroserviceResponseDto<ICollection<ListingViewDto>>> GetUserListings(string userId);
    }
}
