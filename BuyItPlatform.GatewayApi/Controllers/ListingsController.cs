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
        public async Task<ResponseDto<object>> UploadListing([FromForm] ListingDto listingDto)
        {
            return await listingsService.UploadListingAsync<object>(listingDto);
        }

        [HttpGet]
        [Route("GetListingWithId/{listingId:int}")]
        public async Task<ResponseDto<ListingDto>> GetListingWithId(int listingId)
        {
            return await listingsService.GetListingWithIdAsync<ListingDto>(listingId);
        }

        [HttpGet]
        [Route("GetListings")]
        public async Task<ResponseDto<List<ListingDto>>> GetListings([FromQuery] ListingFIlterDto listFilter, [FromQuery] int count, [FromQuery] int offset)
        {
            return await listingsService.GetListingsAsync<List<ListingDto>>(listFilter, count, offset);
        }

        [HttpGet]
        [Route("DeleteListing/{listingId:int}")]
        public async Task<ResponseDto<object>> DeleteListing(int listingId)
        {
            return await listingsService.DeleteListingAsync<object>(listingId);
        }

        [HttpGet]
        [Route("DeleteUserListings/{userid:int}")]
        public async Task<ResponseDto<object>> DeleteUserListings(int userid)
        {
            return await listingsService.DeleteUserListingsAsync<object>(userid);
        }
    }
}
