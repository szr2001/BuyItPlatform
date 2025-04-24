using BuyItPlatform.GatewayApi.Models;

namespace BuyItPlatform.GatewayApi.Services.IServices
{
    public interface IListingsService
    {
        Task<ResponseDto> DeleteListingAsync(int listingId);
        Task<ResponseDto> DeleteUserListingsAsync(int userId);
        Task<ResponseDto> GetListingsAsync(ListingFIlterDto listFilter, int count, int offset);
        Task<ResponseDto> GetListingWithIdAsync(int id);
        Task<ResponseDto> UploadListingAsync(ListingDto listingDto);
    }
}
