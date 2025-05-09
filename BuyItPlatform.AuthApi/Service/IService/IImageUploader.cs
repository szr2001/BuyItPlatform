using BuyItPlatform.AuthApi.Models.Dto;

namespace BuyItPlatform.AuthApi.Services.IServices
{
    public interface IImageUploader
    {
        public Task<string[]> UploadImagesAsync(int Id, ICollection<IFormFile> files);
        public Task DeleteImagesAsync(int Id);
    }
}
