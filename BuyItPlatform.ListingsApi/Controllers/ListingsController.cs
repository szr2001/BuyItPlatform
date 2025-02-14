using AutoMapper;
using BuyItPlatform.ListingsApi.Data;
using BuyItPlatform.ListingsApi.Models;
using BuyItPlatform.ListingsApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.ListingsApi.Controllers
{
    [ApiController]
    [Route("api/listingsApi")]
    public class ListingsController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private ResponseDto responseDto;
        public ListingsController(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            responseDto = new();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                //Listing listing = dbContext.Listings.FirstOrDefault();

                Listing listing = new() {Name = "Chair", Description= "Good chair bro", Category = Enums.Category.ToysHobbies,Color = Enums.Color.Gray, SubCategory = Enums.SubCategory.Accessories, Currency = "eur", Price = 15, ListingType = Enums.TransactionType.Sell };
                responseDto.Result = mapper.Map<ListingDto>(listing);
            }
            catch (Exception ex)
            {
                responseDto.Success = false;
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }
    }
}
