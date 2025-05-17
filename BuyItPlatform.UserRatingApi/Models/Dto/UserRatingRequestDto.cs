using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BuyItPlatform.UserRatingApi.Models.Dto
{
    public class UserRatingRequestDto
    {
        public string TargetUserId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
    }
}
