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

        public async Task<ICollection<Listing>> GetListingsAsync(ListingFIlterDto listFilter, int count, int offset)
        {
            IQueryable<Listing> query = dbContext.Listings.AsQueryable();

            if (!string.IsNullOrEmpty(listFilter.Currency))
            {
                query = query.Where(l => l.Currency == Enum.Parse<Currency>(listFilter.Currency));
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

        public async Task UploadListingAsync(ListingUploadDto listingDto)
        {
            if (string.IsNullOrEmpty(listingDto.Name))
            {
                throw new ArgumentException("A name is required M'lord!");
            }
            if (string.IsNullOrEmpty(listingDto.Description))
            {
                throw new ArgumentException("A description is required M'lord!");
            }
            if (listingDto.ImageFiles.Count > 3)
            {
                throw new ArgumentOutOfRangeException("You can only have a maximum of 3 images.");
            }
            if(listingDto.Tags.Count > 5)
            {
                throw new ArgumentOutOfRangeException("You can only have a maximum of 5 tags.");
            }
            if (!Enum.TryParse<Category>(listingDto.Category,out _))
            {
                throw new ArgumentException("Category doesn't exist or is empty.");
            }
            if(listingDto.Color != null)
            {
                if (!Enum.TryParse<Color>(listingDto.Color, out _))
                {
                    throw new ArgumentException("Color doesn't exist.");
                }
            }
            if (listingDto.SubCategory != null)
            {
                if (!Enum.TryParse<SubCategory>(listingDto.SubCategory, out _))
                {
                    throw new ArgumentException("SubCategory doesn't exist.");
                }
            }
            if (!Enum.TryParse<Currency>(listingDto.Currency, out _))
            {
                throw new ArgumentException("Currency doesn't exist or is empty.");
            }
            foreach(var tag in listingDto.Tags)
            {
                if (!Enum.TryParse<Tag>(tag, out _))
                {
                    throw new ArgumentException($"Tag: '{tag}' doesn't exist.");
                }
            }

            var existingListing = await dbContext.Listings.Where((u) => u.UserId == listingDto.UserId && u.SlotId == listingDto.SlotId).SingleOrDefaultAsync();
            if(existingListing != null)
            {
                throw new ArgumentOutOfRangeException("This slot is already occupated.");
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

        public async Task DeleteListingAsync(string userId,int listingId)
        {
            Listing listing = await dbContext.Listings.Where(i => i.Id == listingId && i.UserId == userId).FirstAsync();
            if(listing != null)
            {
                await imageUploader.DeleteImagesAsync(listingId.ToString());
                dbContext.Listings.Remove(listing);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("listing doesn't exist or you don't own that listing");
            }
        }

        public async Task DeleteUserListingsAsync(string userId)
        {
            var listing = await dbContext.Listings.Where(i => i.UserId == userId).ToListAsync();
            dbContext.Listings.RemoveRange(listing);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Listing>> GetUserListings(string userId)
        {
            return await dbContext.Listings.Where((e) => e.UserId == userId).ToListAsync();
        }
    }
}
