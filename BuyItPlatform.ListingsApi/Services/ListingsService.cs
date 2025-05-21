using AutoMapper;
using BuyItPlatform.ListingsApi.Data;
using BuyItPlatform.ListingsApi.Enums;
using BuyItPlatform.ListingsApi.Models;
using BuyItPlatform.ListingsApi.Models.Dto;
using BuyItPlatform.ListingsApi.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BuyItPlatform.ListingsApi.Services
{
    public class ListingsService : IListingsService
    {
        private readonly AppDbContext dbContext;
        private readonly IImageUploader imageUploader;
        private readonly IMapper mapper;
        public ListingsService(AppDbContext dbContext, IMapper mapper, IImageUploader imageUploader)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.imageUploader = imageUploader;
        }

        public async Task<Listing> GetListingWithIdAsync(int id)
        {
            return await dbContext.Listings.Where(i => i.Id == id).FirstAsync();
        }

        public async Task<List<Listing>> GetListingsAsync(ListingFIlterDto listFilter, int count, int offset)
        {
            IQueryable<Listing> query = dbContext.Listings.AsQueryable();

            if (!string.IsNullOrEmpty(listFilter.Currency))
            {
                query = query.Where(l => l.Currency == listFilter.Currency);
            }

            if (!string.IsNullOrEmpty(listFilter.Category))
            {
                query = query.Where(l => l.Category == Enum.Parse<Category>(listFilter.Category));
            }

            if (!string.IsNullOrEmpty(listFilter.SubCategory))
            {
                query = query.Where(l => l.SubCategory == Enum.Parse<SubCategory>(listFilter.SubCategory));
            }

            if (!string.IsNullOrEmpty(listFilter.ListingType))
            {
                query = query.Where(l => l.ListingType == Enum.Parse<TransactionType>(listFilter.ListingType));
            }

            if (!string.IsNullOrEmpty(listFilter.Color))
            {
                query = query.Where(l => l.Color == Enum.Parse<Color>(listFilter.Color));
            }

            if (listFilter.Tags.Count > 0)
            {
                query = query.Where(l => listFilter.Tags.All(tag => l.Tags.Select(t => t.ToString()).Contains(tag)));
            }

            if (!string.IsNullOrEmpty(listFilter.Name))
            {
                query = query.Where(l => EF.Functions.Like(l.Name, $"%{listFilter.Name}%"));
            }

            if (listFilter.MinPrice >= 0)
            {
                query = query.Where(l => l.Price >= listFilter.MinPrice);
            }

            if (listFilter.MinPrice < listFilter.MaxPrice)
            {
                query = query.Where(l => l.Price <= listFilter.MaxPrice);
            }

            List<Listing> listings = await query.Skip(offset).Take(count).ToListAsync();

            return listings;
        }

        public async Task UploadListingAsync(ListingDto listingDto)
        {
            if (listingDto.ImageFiles.Count > 3)
            {
                throw new ArgumentOutOfRangeException("Each listing can have a maximum of 3 images.");
            }

            Listing newListing = mapper.Map<Listing>(listingDto);

            dbContext.Listings.Add(newListing);
            await dbContext.SaveChangesAsync();

            if (listingDto.ImageFiles.Count > 0)
            {
                newListing.ImagePaths = await imageUploader.UploadImagesAsync(newListing.Id.ToString(), listingDto.ImageFiles);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteListingAsync(int listingId)
        {
            await imageUploader.DeleteImagesAsync(listingId.ToString());
            Listing listing = await dbContext.Listings.Where(i => i.Id == listingId).FirstAsync();
            dbContext.Listings.Remove(listing);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserListingsAsync(int userId)
        {
            var listing = await dbContext.Listings.Where(i => i.UserId == userId).ToListAsync();
            dbContext.Listings.RemoveRange(listing);
            await dbContext.SaveChangesAsync();
        }
    }
}
