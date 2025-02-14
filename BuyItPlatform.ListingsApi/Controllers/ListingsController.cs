using AutoMapper;
using BuyItPlatform.ListingsApi.Data;
using BuyItPlatform.ListingsApi.Models;
using BuyItPlatform.ListingsApi.Models.Dto;
using BuyItPlatform.ListingsApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.ListingsApi.Controllers
{
    [ApiController]
    [Route("api/listingsApi")]
    public class ListingsController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IImageUploader imageUploader;
        private ResponseDto response;
        public ListingsController(AppDbContext dbContext, IMapper mapper, IImageUploader imageUploader)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.imageUploader = imageUploader;
            response = new();
        }

        [HttpPost]
        public ResponseDto Post([FromBody] ListingDto listing)
        {
            try
            {

            }
            catch(Exception ex)
            {

            }

            return response;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                //Listing listing = dbContext.Listings.FirstOrDefault();

                Listing listing = new() {Name = "Chair", Description= "Good chair bro", Category = Enums.Category.ToysHobbies,Color = Enums.Color.Gray, SubCategory = Enums.SubCategory.Accessories, Currency = "eur", Price = 15, ListingType = Enums.TransactionType.Sell };
                response.Result = mapper.Map<ListingDto>(listing);
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
