using AutoMapper;
using BuyItPlatform.ListingsApi.Data;
using BuyItPlatform.ListingsApi.Enums;
using BuyItPlatform.ListingsApi.Models;
using BuyItPlatform.ListingsApi.Models.Dto;
using BuyItPlatform.ListingsApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BuyItPlatform.ListingsApi.Controllers
{
    [ApiController]
    [Route("api/listingsApi")]
    public class ListingsController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IImageUploader imageUploader;
        private readonly IListingService listingService;
        private ResponseDto response;
        public ListingsController(AppDbContext dbContext, IMapper mapper, IImageUploader imageUploader, IListingService listingService)
        {
            response = new();
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.imageUploader = imageUploader;
            this.listingService = listingService;
        }

        [HttpPost]
        [Route("UploadListing")]    
        public async Task<ResponseDto> UploadListing([FromForm] ListingDto listingDto)
        {
            try
            {
                if(listingDto.ImageFiles.Count > 3)
                {
                    response.Success = false;
                    response.Message = "Each listing can have a maximum of 3 images";
                    return response;
                }

                Listing newListing = mapper.Map<Listing>(listingDto);

                dbContext.Listings.Add(newListing);
                await dbContext.SaveChangesAsync();

                if(listingDto.ImageFiles.Count > 0)
                {
                    var result = await imageUploader.UploadImagesAsync(newListing.Id, listingDto.ImageFiles);
                    if(result.Success)
                    {
                        newListing.ImagePaths = (string[])result.Result!;
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message += ex.Message;
            }

            return response;
        }

        [HttpGet]
        [Route("GetListingWithId/{listingId:int}")]
        public async Task<ResponseDto> GetListingWithId(int listingId)
        {
            try
            {
                Listing listing = await dbContext.Listings.Where(i => i.Id == listingId).FirstAsync();

                ListingDto responseDto = mapper.Map<ListingDto>(listing);

                response.Result = responseDto;
                response.Success = true;
                
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet]
        [Route("GetListings")]
        public async Task<ResponseDto> GetListings([FromQuery] ListingFIlterDto listFilter, [FromQuery] int count, [FromQuery] int offset)
        {
            //move into service and use helper methods
            try
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

                if(listFilter.MinPrice >= 0)
                {
                    query = query.Where(l => l.Price >= listFilter.MinPrice);
                }
                
                if(listFilter.MinPrice < listFilter.MaxPrice)
                {
                    query = query.Where(l => l.Price <= listFilter.MaxPrice);
                }

                List<Listing> listings = await query.Skip(offset).Take(count).ToListAsync();

                response.Success = true;
                response.Result = listings;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
