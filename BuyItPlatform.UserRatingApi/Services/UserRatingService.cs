using BuyItPlatform.UserRatingApi.Data;
using BuyItPlatform.UserRatingApi.Models;
using BuyItPlatform.UserRatingApi.Models.Dto;
using BuyItPlatform.UserRatingApi.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BuyItPlatform.UserRatingApi.Services
{
    public class UserRatingService : IUserRatingService
    {
        private readonly AppDbContext dbContext;

        public UserRatingService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteOfferedRatings(string userId)
        {
            var ratings = dbContext.Ratings.Where(r => r.UserId == userId);

            dbContext.Ratings.RemoveRange(ratings);
            await dbContext.SaveChangesAsync();
        }

        public async Task<UserRatingResponseDto> GetUserRating(string targetUserId)
        {
            UserRatingResponseDto userRating = new();

            var ratings = dbContext.Ratings.Where(r => r.TargetUserId == targetUserId);

            if(ratings.Any())
            {
                var averageRating = await ratings.AverageAsync(r => r.Rating);
                var numberOfRatings = await ratings.CountAsync();
                userRating.AverageRating = (int)averageRating;
                userRating.NumberOfRatings = numberOfRatings;
            }

            return userRating;
        }

        public async Task RateUser(UserRatingRequestDto ratingRequest)
        {
            var existingRating = await dbContext.Ratings.FirstOrDefaultAsync(e => e.UserId == ratingRequest.UserId && e.TargetUserId == ratingRequest.TargetUserId);

            if(existingRating != null)
            {
                existingRating.Rating = ratingRequest.Rating;
            }
            else
            {
                UserRating rating = new() 
                {
                    Rating = ratingRequest.Rating,
                    TargetUserId = ratingRequest.TargetUserId,
                    UserId = ratingRequest.UserId
                };
                dbContext.Ratings.Add(rating);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
