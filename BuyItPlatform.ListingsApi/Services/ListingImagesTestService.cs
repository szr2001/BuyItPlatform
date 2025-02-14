using BuyItPlatform.ListingsApi.Models.Dto;
using BuyItPlatform.ListingsApi.Services.IServices;

namespace BuyItPlatform.ListingsApi.Services
{
    //fakes uploading images to platforms like AWS blob storage or S3 and returing the link
    //then save the link in the database alongside the Listing data.
    //when we delete the listings, we use the links to also delete the images from the aws storage
    public class ListingImagesTestService : IImageUploader
    {
        private ResponseDto response;

        public async Task<ResponseDto> DeleteImagesAsync(int listingId)
        {
            await Task.Delay(100);
            return response;
        }

        public async Task<ResponseDto> UploadImageAsync(int listingId, IFormFile file)
        {
            response.Result = $"https://FakePath/{listingId}/{Guid.NewGuid}";
            response.Success = true;
            await Task.Delay(100);
            return response;
        }

        public async Task<ResponseDto> UploadImagesAsync(int listingId, IFormFile[] files)
        {
            response.Result = new List<string>() { $"https://FakePath/{listingId}/{Guid.NewGuid}", $"https://FakePath/{listingId}/{Guid.NewGuid}", $"https://FakePath/{listingId}/{Guid.NewGuid}" };
            response.Success = true;

            await Task.Delay(100);
            return response;
        }
    }
}
