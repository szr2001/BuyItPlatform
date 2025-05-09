using BuyItPlatform.AuthApi.Services.IServices;

namespace BuyItPlatform.AuthApi.Services
{
    //fakes uploading images to platforms like AWS blob storage or S3 and returing the link
    //then save the link in the database alongside the Listing data.
    //when we delete the listings, we use the links to also delete the images from the aws storage
    public class ImageUploaderMockService : IImageUploader
    {
        private string testPath = @"";
        //move to appsettings
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly string[] _allowedMimeTypes = { "image/jpeg", "image/png" };
        public ImageUploaderMockService()
        {
            testPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public async Task DeleteImagesAsync(int id)
        {
            List<string> imagesToDelete = new();
            foreach (var filePath in Directory.GetFiles(testPath))
            {
                string[] path = filePath.Split('_');
                if (path[0] == $"{testPath}\\{id}")
                {
                    imagesToDelete.Add(filePath);
                }
            }

            foreach (var deleteImage in imagesToDelete)
            {
                File.Delete(deleteImage);
            }
        }

        public async Task<string[]> UploadImagesAsync(int id, ICollection<IFormFile> files)
        {
            List<string> paths = new();

            //verify files before saving
            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                {
                    throw new Exception("File not specified.");
                }

                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                string mimeType = file.ContentType.ToLower();

                if (!_allowedExtensions.Contains(fileExtension) || !_allowedMimeTypes.Contains(mimeType))
                {
                    throw new Exception("Only JPG and PNG files are allowed");
                }
            }

            //save files
            foreach (var file in files)
            {
                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                string mimeType = file.ContentType.ToLower();

                string fileName = $"{id}_{Guid.NewGuid()}{fileExtension}";
                string filePath = Path.Combine(testPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                paths.Add(filePath);
            }

            return paths.ToArray();
        }
    }
}
