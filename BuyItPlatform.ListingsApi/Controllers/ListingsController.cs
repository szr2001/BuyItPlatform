using AutoMapper;
using BuyItPlatform.ListingsApi.Models;
using BuyItPlatform.ListingsApi.Models.Dto;
using BuyItPlatform.ListingsApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.ListingsApi.Controllers
{
    [ApiController]
    [Route("listingsApi")]
    //[Authorize]
    public class ListingsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IListingsService listingService;
        private ResponseDto response = new();
        public ListingsController(IMapper mapper, IListingsService listingService)
        {
            response = new();
            this.mapper = mapper;
            this.listingService = listingService;
        }

        [HttpPost]
        [Route("uploadListing")]    
        public async Task<IActionResult> UploadListing([FromForm] ListingDto listingDto)
        {
            try
            {
                await listingService.UploadListingAsync(listingDto);
                response.Result = null;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message += ex.Message;
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("getUserListings/{userId}")]
        public async Task<IActionResult> GetUserListings(string userId)
        {
            try
            {
                ICollection<Listing> listing = await listingService.GetUserListings(userId);
                List<ListingDto> responseDto = mapper.Map<List<ListingDto>>(listing);
                response.Result = responseDto;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("getListingWithId/{listingId:int}")]
        public async Task<IActionResult> GetListingWithId(int listingId)
        {
            try
            {
                Listing listing = await listingService.GetListingWithIdAsync(listingId);
                ListingDto responseDto = mapper.Map<ListingDto>(listing);
                response.Result = responseDto;
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("getListings")]
        public async Task<IActionResult> GetListings([FromBody] ListingFIlterDto listFilter, [FromQuery] int count, [FromQuery] int offset)
        {
            try
            {
                ICollection<Listing> listings = await listingService.GetListingsAsync(listFilter, count, offset);
                List<ListingDto> listingDtos = mapper.Map<List<ListingDto>>(listings);
                response.Result = listingDtos;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("deleteListing/{listingId:int}")]
        public async Task<IActionResult> DeleteListing(int listingId)
        {
            try
            {
                await listingService.DeleteListingAsync(listingId);
                response.Result = null;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("deleteUserListings/{userid}")]
        public async Task<IActionResult> DeleteUserListings(string userid)
        {
            try
            {
                await listingService.DeleteUserListingsAsync(userid);
                response.Result = null;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
