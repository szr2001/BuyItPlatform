using BuyItPlatform.ListingsApi.Services.IServices;

namespace BuyItPlatform.ListingsApi.Services
{
    //fakes uploading images to platforms like AWS blob storage or S3 and returing the link
    //then save the link in the database alongside the Listing data.
    //when we delete the listings, we use the links to also delete the images from the aws storage
    public class ImageUploaderMockService : IImageUploader
    {
        private string testPath = @"";
        private string testAdress = @"";
        //move to appsettings
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly string[] _allowedMimeTypes = { "image/jpeg", "image/png" };
        private readonly IWebHostEnvironment environment;

        public ImageUploaderMockService(IWebHostEnvironment env)
        {
            environment = env;
            //set the api path for mocking
            //serving the images from the microservices
            //in production we replace this implementation
            //to something like AWS blob storage
            testPath = $@"{environment.ContentRootPath}\ListingPicsMock\";
            testAdress = @"https://localhost:7002/ListingPicsMock/";
        }

        public async Task DeleteImagesAsync(string id)
        {
            List<string> imagesToDelete = new();
            foreach (var filePath in Directory.GetFiles(testPath))
            {
                if (filePath.Contains($"{id}"))
                {
                    imagesToDelete.Add(filePath);
                }
            }

            foreach (var deleteImage in imagesToDelete)
            {
                File.Delete(deleteImage);
            }
        }

        public async Task<string[]> UploadImagesAsync(string id, ICollection<IFormFile> files)
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

                //set the file to the path project
                //set the localhost in the database
                string fileName = $"{id}_{Guid.NewGuid()}{fileExtension}";
                string filePath = Path.Combine(testPath, fileName);
                string adresspath = Path.Combine(testAdress, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                paths.Add(adresspath);
            }

            return paths.ToArray();
        }
    }
}
