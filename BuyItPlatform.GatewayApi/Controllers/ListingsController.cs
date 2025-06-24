using BuyItPlatform.GatewayApi.Models.ListingApiDto;
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
        private readonly ICommentsService commentsService;

        public ListingsController(ICommentsService commentsService, IListingsService listingsService)
        {
            this.commentsService = commentsService;
            this.listingsService = listingsService;
        }

        [HttpPost]
        [Route("uploadListing")]
        public async Task<IActionResult> UploadListing([FromForm] ListingUploadDto listingDto)
        {
            var apiResult = await listingsService.UploadListingAsync(listingDto);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("getListingWithId/{listingId}")]
        public async Task<IActionResult> GetListingWithId(string listingId)
        {
            var apiResult = await listingsService.GetListingWithIdAsync(listingId);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("getListings")]
        public async Task<IActionResult> GetListings([FromBody] ListingFIlterDto listFilter, [FromQuery] int count, [FromQuery] int offset)
        {
            var apiResult = await listingsService.GetListingsAsync(listFilter, count, offset);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("countListings")]
        public async Task<IActionResult> CountListings([FromBody] ListingFIlterDto listFilter)
        {
            var apiResult = await listingsService.CountListingsAsync(listFilter);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("deleteListing/{listingId}")]
        public async Task<IActionResult> DeleteListing(string listingId)
        {
            var apiResult = await listingsService.DeleteListingAsync(listingId);
            if (apiResult.Success)
            {
                await commentsService.DeleteListingCommentsAsync(listingId);
            }
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("deleteUserListings")]
        public async Task<IActionResult> DeleteUserListings()
        {
            var apiResult = await listingsService.DeleteUserListingsAsync();
            return StatusCode(apiResult.StatusCode, apiResult);
        }
    }
}
