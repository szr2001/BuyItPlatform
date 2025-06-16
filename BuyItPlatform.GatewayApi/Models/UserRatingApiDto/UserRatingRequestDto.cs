using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BuyItPlatform.GatewayApi.Models.UserRatingApiDto
{
    public class UserRatingRequestDto
    {
        public string TargetUserId { get; set; } = "";
        public int Rating { get; set; }
    }
}
