using BuyItPlatform.ListingsApi.Models.Dto;

namespace BuyItPlatform.ListingsApi.Services.IServices
{
    public interface IImageUploader
    {
        public Task<string[]> UploadImagesAsync(int listingId, ICollection<IFormFile> files);
        public Task DeleteImagesAsync(int listingId);
    }
}
