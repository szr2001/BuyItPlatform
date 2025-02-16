using BuyItPlatform.ListingsApi.Models.Dto;

namespace BuyItPlatform.ListingsApi.Services.IServices
{
    public interface IImageUploader
    {
        public Task<ResponseDto> UploadImagesAsync(int listingId, ICollection<IFormFile> files);
        public Task<ResponseDto> DeleteImagesAsync(int listingId);
    }
}
