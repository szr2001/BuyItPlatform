using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/listingsApi")]
    [ApiController]
    public class ListingsController : Controller
    {
        private readonly IListingsService listingsService;
        public ListingsController(IListingsService listingsService)
        {
            this.listingsService = listingsService;
        }
        [HttpPost]
        [Route("UploadListing")]
        public async Task<ResponseDto> UploadListing([FromForm] ListingDto listingDto)
        {
            return await listingsService.UploadListingAsync(listingDto);
        }

        [HttpGet]
        [Route("GetListingWithId/{listingId:int}")]
        public async Task<ResponseDto> GetListingWithId(int listingId)
        {
            return await listingsService.GetListingWithIdAsync(listingId);
        }

        [HttpGet]
        [Route("GetListings")]
        public async Task<ResponseDto> GetListings([FromQuery] ListingFIlterDto listFilter, [FromQuery] int count, [FromQuery] int offset)
        {
            return await listingsService.GetListingsAsync(listFilter, count, offset);
        }

        [HttpGet]
        [Route("DeleteListing/{listingId:int}")]
        public async Task<ResponseDto> DeleteListing(int listingId)
        {
            return await listingsService.DeleteListingAsync(listingId);
        }

        [HttpGet]
        [Route("DeleteUserListings/{userid:int}")]
        public async Task<ResponseDto> DeleteUserListings(int userid)
        {
            return await listingsService.DeleteUserListingsAsync(userid);
        }
    }
}
