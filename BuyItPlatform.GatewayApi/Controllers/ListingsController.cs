using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var apiResult = await listingsService.UploadListingAsync<object>(listingDto);
            return StatusCode((int)apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("getListingWithId/{listingId:int}")]
        public async Task<IActionResult> GetListingWithId(int listingId)
        {
            var apiResult = await listingsService.GetListingWithIdAsync<ListingDto>(listingId);
            return StatusCode((int)apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("getListings")]
        public async Task<IActionResult> GetListings([FromBody] ListingFIlterDto listFilter, [FromQuery] int count, [FromQuery] int offset)
        {
            var apiResult = await listingsService.GetListingsAsync<List<ListingDto>>(listFilter, count, offset);
            return StatusCode((int)apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("deleteListing/{listingId:int}")]
        public async Task<IActionResult> DeleteListing(int listingId)
        {
            var apiResult = await listingsService.DeleteListingAsync<object>(listingId);
            return StatusCode((int)apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("deleteUserListings/{userid:int}")]
        public async Task<IActionResult> DeleteUserListings(int userid)
        {
            var apiResult = await listingsService.DeleteUserListingsAsync<object>(userid);
            return StatusCode((int)apiResult.StatusCode, apiResult);
        }
    }
}
