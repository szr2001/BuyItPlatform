using BuyItPlatform.GatewayApi.Models;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IListingsService
    {
        Task<MicroserviceResponseDto<T>> DeleteListingAsync<T>(int listingId);
        Task<MicroserviceResponseDto<T>> DeleteUserListingsAsync<T>(int userId);
        Task<MicroserviceResponseDto<T>> GetListingsAsync<T>(ListingFIlterDto listFilter, int count, int offset);
        Task<MicroserviceResponseDto<T>> GetListingWithIdAsync<T>(int id);
        Task<MicroserviceResponseDto<T>> UploadListingAsync<T>(ListingDto listingDto);
    }
}
