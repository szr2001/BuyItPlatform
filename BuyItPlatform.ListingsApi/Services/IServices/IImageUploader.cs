﻿using BuyItPlatform.ListingsApi.Models.Dto;

namespace BuyItPlatform.ListingsApi.Services.IServices
{
    public interface IImageUploader
    {
        public Task<ResponseDto> UploadImageAsync(int listingId, IFormFile file);
        public Task<ResponseDto> UploadImagesAsync(int listingId, IFormFile[] files);
        public Task<ResponseDto> DeleteImagesAsync(int listingId);
    }
}
