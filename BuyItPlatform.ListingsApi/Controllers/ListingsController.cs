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
    [Authorize]
    public class ListingsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IListingsService listingService;
        private readonly ITokenCookiesProvider tokenCookiesProvider;
        private readonly IJwtTokenHandler jwtTokenHandler;
        private ResponseDto response = new();
        public ListingsController(IMapper mapper, IListingsService listingService, IJwtTokenHandler jwtTokenHandler, ITokenCookiesProvider tokenCookiesProvider)
        {
            response = new();
            this.mapper = mapper;
            this.listingService = listingService;
            this.jwtTokenHandler = jwtTokenHandler;
            this.tokenCookiesProvider = tokenCookiesProvider;
        }

        [HttpPost]
        [Route("uploadListing")]    
        public async Task<IActionResult> UploadListing([FromForm] ListingUploadDto listingDto)
        {
            try
            {
                var Token = tokenCookiesProvider.GetToken();
                var tokenData = jwtTokenHandler.ExtractTokenData(Token);
                var userId = tokenData.Where(i => i.Type == "nameid").First().Value;
                listingDto.UserId = userId;

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
                List<ListingViewDto> responseDto = mapper.Map<List<ListingViewDto>>(listing);
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
                ListingViewDto responseDto = mapper.Map<ListingViewDto>(listing);
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
                List<ListingViewDto> listingDtos = mapper.Map<List<ListingViewDto>>(listings);
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

        [HttpPost]
        [Route("countListings")]
        public async Task<IActionResult> CountListingsAsync([FromBody] ListingFIlterDto listFilter)
        {
            try
            {
                response.Result = await listingService.CountListingsAsync(listFilter);
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
                var Token = tokenCookiesProvider.GetToken();
                var tokenData = jwtTokenHandler.ExtractTokenData(Token);
                var userId = tokenData.Where(i => i.Type == "nameid").First().Value;
                await listingService.DeleteListingAsync(userId, listingId);
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
        [Route("deleteUserListings")]
        public async Task<IActionResult> DeleteUserListings()
        {
            try
            {
                var Token = tokenCookiesProvider.GetToken();
                var tokenData = jwtTokenHandler.ExtractTokenData(Token);
                var userId = tokenData.Where(i => i.Type == "nameid").First().Value;
                await listingService.DeleteUserListingsAsync(userId);
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
