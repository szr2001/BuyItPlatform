using BuyItPlatform.GatewayApi.Models;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IListingsService
    {
        Task<ResponseDto<T>> DeleteListingAsync<T>(int listingId);
        Task<ResponseDto<T>> DeleteUserListingsAsync<T>(int userId);
        Task<ResponseDto<T>> GetListingsAsync<T>(ListingFIlterDto listFilter, int count, int offset);
        Task<ResponseDto<T>> GetListingWithIdAsync<T>(int id);
        Task<ResponseDto<T>> UploadListingAsync<T>(ListingDto listingDto);
    }
}
