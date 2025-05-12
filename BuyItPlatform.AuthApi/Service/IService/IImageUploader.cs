using BuyItPlatform.AuthApi.Models.Dto;

namespace BuyItPlatform.AuthApi.Services.IServices
{
    public interface IImageUploader
    {
        public Task<string[]> UploadImagesAsync(string Id, ICollection<IFormFile> files);
        public Task DeleteImagesAsync(string Id);
    }
}
