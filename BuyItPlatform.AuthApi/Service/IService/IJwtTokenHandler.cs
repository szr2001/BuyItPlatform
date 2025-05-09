using BuyItPlatform.AuthApi.Models;
using BuyItPlatform.AuthApi.Models.Dto;
using System.Security.Claims;

namespace BuyItPlatform.AuthApi.Service.IService
{
    public interface IJwtTokenHandler
    {
        string GenerateRefreshToken();
        string GenerateToken(BuyItUser user, IEnumerable<string> roles);
        ICollection<Claim> ExtractTokenData(string token);
    }
}
