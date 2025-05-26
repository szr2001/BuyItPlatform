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

        public async Task DeleteOfferedRatingsAsync(string userId)
        {
            var ratings = await dbContext.Ratings.Where(r => r.UserId == userId).FirstOrDefaultAsync();
            if (ratings == null)
            {
                throw new ArgumentNullException("UserId not found");
            }

            dbContext.Ratings.RemoveRange(ratings);
            await dbContext.SaveChangesAsync();
        }

        public async Task<UserRatingResponseDto> GetUserRatingAsync(string targetUserId)
        {
            UserRatingResponseDto result = new();

            var averageRating = await dbContext.Ratings
                    .Where(r => r.TargetUserId == targetUserId)
                    .AverageAsync(r => (double?)r.Rating) ?? 0;

            var numberOfRatings = await dbContext.Ratings
                .CountAsync(r => r.TargetUserId == targetUserId);

            if(numberOfRatings != 0)
            {
                result.AverageRating = (int)averageRating;
                result.NumberOfRatings = numberOfRatings;
            }

            return result;
        }

        public async Task<UserRatingResponseDto[]> GetUsersScoreboardAsync(int count, int offset)
        {
            return await dbContext.Ratings
                    .GroupBy(r => r.TargetUserId)
                    .Select(g => new UserRatingResponseDto
                    {
                        TargetUserId = g.Key,
                        AverageRating = (int)g.Average(r => r.Rating),
                        NumberOfRatings = g.Count()
                    })
                    .OrderByDescending(dto => dto.NumberOfRatings)
                    .Skip(offset)
                    .Take(count)
                    .ToArrayAsync();
        }

        public async Task RateUserAsync(UserRatingRequestDto ratingRequest)
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
