﻿using BuyItPlatform.ListingsApi.Models.Dto;

namespace BuyItPlatform.ListingsApi.Services.IServices
{
    public interface IImageUploader
    {
        public Task<string[]> UploadImagesAsync(int Id, ICollection<IFormFile> files);
        public Task DeleteImagesAsync(int Id);
    }
}
