using Microsoft.AspNetCore.Identity;

namespace BuyItPlatform.AuthApi.Models
{
    public class BuyItUser : IdentityUser
    {
        public string RefreshToken { get; set; } = "";
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
