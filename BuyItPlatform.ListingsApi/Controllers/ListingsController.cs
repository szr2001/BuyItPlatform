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
        private ResponseDto responseDto;
        public ListingsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            responseDto = new();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                Listing listing = dbContext.Listings.FirstOrDefault();
                responseDto.Result = listing;
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
