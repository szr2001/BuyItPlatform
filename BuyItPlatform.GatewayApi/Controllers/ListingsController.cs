﻿using BuyItPlatform.GatewayApi.Models.ListingApiDto;
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
        public async Task<IActionResult> UploadListing([FromForm] ListingUploadDto listingDto)
        {
            var apiResult = await listingsService.UploadListingAsync(listingDto);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("getListingWithId/{listingId:int}")]
        public async Task<IActionResult> GetListingWithId(int listingId)
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
        public async Task<IActionResult> CountListingsAsync([FromBody] ListingFIlterDto listFilter)
        {
            var apiResult = await listingsService.CountListingsAsync(listFilter);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpGet]
        [Route("deleteListing/{listingId:int}")]
        public async Task<IActionResult> DeleteListing(int listingId)
        {
            var apiResult = await listingsService.DeleteListingAsync(listingId);
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
