using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BuyItPlatform.AuthApi.Models
{
    public class BuyItUser : IdentityUser
    {
        [MaxLength(350)]
        [NotNull]
        public string Description { get; set; } = " ";
        [MaxLength(150)]
        [NotNull]
        public string ProfileImgLink { get; set; } = " ";
        [MaxLength(150)]
        public string RefreshToken { get; set; } = " ";
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
