using BuyItPlatform.ListingsApi.Models.Dto;

namespace BuyItPlatform.ListingsApi.Services.IServices
{
    public interface IImageUploader
    {
        public Task<string[]> UploadImagesAsync(string Id, ICollection<IFormFile> files);
        public Task DeleteImagesAsync(string Id);
    }
}
