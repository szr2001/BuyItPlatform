using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/listingsApi")]
    [Authorize]
    [ApiController]
    public class ListingsController : Controller
    {
        private readonly IListingsService listingsService;
        public ListingsController(IListingsService listingsService)
        {
            this.listingsService = listingsService;
        }

        [HttpPost]
        [Route("uploadListing")]
        public async Task<IActionResult> UploadListing([FromForm] ListingDto listingDto)
        {
            return Ok(await listingsService.UploadListingAsync<object>(listingDto));
        }

        [HttpGet]
        [Route("getListingWithId/{listingId:int}")]
        public async Task<IActionResult> GetListingWithId(int listingId)
        {
            return Ok(await listingsService.GetListingWithIdAsync<ListingDto>(listingId));
        }

        [HttpPost]
        [Route("getListings")]
        public async Task<IActionResult> GetListings([FromBody] ListingFIlterDto listFilter, [FromQuery] int count, [FromQuery] int offset)
        {
            return Ok(await listingsService.GetListingsAsync<List<ListingDto>>(listFilter, count, offset));
        }

        [HttpGet]
        [Route("deleteListing/{listingId:int}")]
        public async Task<IActionResult> DeleteListing(int listingId)
        {
            return Ok(await listingsService.DeleteListingAsync<object>(listingId));
        }

        [HttpGet]
        [Route("deleteUserListings/{userid:int}")]
        public async Task<IActionResult> DeleteUserListings(int userid)
        {
            return Ok(await listingsService.DeleteUserListingsAsync<object>(userid));
        }
    }
}
