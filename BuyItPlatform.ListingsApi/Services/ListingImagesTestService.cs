using BuyItPlatform.ListingsApi.Models.Dto;
using BuyItPlatform.ListingsApi.Services.IServices;

namespace BuyItPlatform.ListingsApi.Services
{
    //fakes uploading images to platforms like AWS blob storage or S3 and returing the link
    //then save the link in the database alongside the Listing data.
    //when we delete the listings, we use the links to also delete the images from the aws storage
    public class ListingImagesTestService : IImageUploader
    {
        private string testPath = @"";
        private ResponseDto response = new();
        //move to appsettings
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly string[] _allowedMimeTypes = { "image/jpeg", "image/png" };
        public ListingImagesTestService()
        {
            testPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        //rewrite without responseDto, just return the object, separation of concerns
        public async Task<ResponseDto> DeleteImagesAsync(int listingId)
        {
            List<string> imagesToDelete = new();
            try
            {
                foreach(var filePath in Directory.GetFiles(testPath))
                {
                    if (filePath.Split('_').Contains(listingId.ToString()))
                    {
                        imagesToDelete.Add(filePath);
                    }
                }

                foreach(var deleteImage in imagesToDelete)
                {
                    File.Delete(deleteImage);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            response.Success = true;
            return response;
        }

        public async Task<ResponseDto> UploadImagesAsync(int listingId, ICollection<IFormFile> files)
        {
            List<string> paths = new();
            foreach(var file in files)
            {
                try
                {
                    if (file == null || file.Length == 0)
                    {
                        return new ResponseDto { Success = false, Message = "Invalid file." };
                    }

                    string fileExtension = Path.GetExtension(file.FileName).ToLower();
                    string mimeType = file.ContentType.ToLower();

                    if (!_allowedExtensions.Contains(fileExtension) || !_allowedMimeTypes.Contains(mimeType))
                    {
                        return new ResponseDto { Success = false, Message = "Only JPG and PNG files are allowed." };
                    }
                    string fileName = $"{listingId}_{Guid.NewGuid()}{fileExtension}";
                    string filePath = Path.Combine(testPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    paths.Add(filePath);
                }
                catch(Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                    break;
                }
            }
            response.Result = paths.ToArray();
            response.Success = true;
            return response;
        }
    }
}
