﻿using BuyItPlatform.GatewayApi.Models;
using BuyItPlatform.GatewayApi.Models.Dto;
using BuyItPlatform.GatewayApi.Service.IService;
using BuyItPlatform.GatewayApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BuyItPlatform.GatewayApi.Controllers
{
    [Route("gateway/authApi/user")]
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserRatingService userRatingService;
        public UserController(IUserService userService, IUserRatingService userRatingService)
        {
            this.userService = userService;
            this.userRatingService = userRatingService;
        }

        [HttpGet]
        [Route("getUserProfile/{userId}")]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            var apiResult = await userService.GetUserProfileAsync<UserProfileDto>(userId);
            if (apiResult.Success && apiResult.Result != null)
            {
                var ratingResult = await userRatingService.GetUserRating<UserRatingResponseDto>(apiResult.Result.Id);
                if (ratingResult.Success && ratingResult.Result != null)
                {
                    apiResult.Result.AverageRating = ratingResult.Result.AverageRating;
                    apiResult.Result.NumberOfRatings = ratingResult.Result.NumberOfRatings;
                }
            }
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("updateUserDesc/{desc}")]
        public async Task<IActionResult> UpdateUserDesc(string desc)
        {
            var apiResult = await userService.UpdateUserDescAsync<object>(desc);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("updateUserName/{name}")]
        public async Task<IActionResult> UpdateUserName(string name)
        {
            var apiResult = await userService.UpdateUserNameAsync<object>(name);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("updateUserPhoneNumber/{phoneNumber}")]
        public async Task<IActionResult> UpdateUserPhoneNumber(string phoneNumber)
        {
            var apiResult = await userService.UpdateUserPhoneNumberAsync<object>(phoneNumber);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpPost]
        [Route("updateUserProfilePic")] // FromForm because we expect an image in Binary format
        public async Task<IActionResult> UpdateUserProfilePic([FromForm] ImageDto profilePic)
        {
            var apiResult = await userService.UpdateUserProfilePicsAsync<string>(profilePic);
            return StatusCode(apiResult.StatusCode, apiResult);
        }
    }
}
