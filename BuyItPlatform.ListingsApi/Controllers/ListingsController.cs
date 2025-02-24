﻿using AutoMapper;
using BuyItPlatform.ListingsApi.Models;
using BuyItPlatform.ListingsApi.Models.Dto;
using BuyItPlatform.ListingsApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyItPlatform.ListingsApi.Controllers
{
    [ApiController]
    [Route("api/listingsApi")]
    [Authorize]
    public class ListingsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IListingsService listingService;
        private ResponseDto response;
        public ListingsController(IMapper mapper, IListingsService listingService)
        {
            response = new();
            this.mapper = mapper;
            this.listingService = listingService;
        }

        [HttpPost]
        [Route("UploadListing")]    
        public async Task<ResponseDto> UploadListing([FromForm] ListingDto listingDto)
        {
            try
            {
                await listingService.UploadListingAsync(listingDto);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message += ex.Message;
            }

            return response;
        }

        [HttpGet]
        [Route("GetListingWithId/{listingId:int}")]
        public async Task<ResponseDto> GetListingWithId(int listingId)
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
            }

            return response;
        }

        [HttpGet]
        [Route("GetListings")]
        public async Task<ResponseDto> GetListings([FromQuery] ListingFIlterDto listFilter, [FromQuery] int count, [FromQuery] int offset)
        {
            try
            {
                response.Result = await listingService.GetListingsAsync(listFilter, count, offset);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet]
        [Route("DeleteListing/{listingId:int}")]
        public async Task<ResponseDto> DeleteListing(int listingId)
        {
            try
            {
                await listingService.DeleteListingAsync(listingId);

                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet]
        [Route("DeleteUserListings/{userid:int}")]
        public async Task<ResponseDto> DeleteUserListings(int userid)
        {
            try
            {
                await listingService.DeleteUserListingsAsync(userid);

                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
