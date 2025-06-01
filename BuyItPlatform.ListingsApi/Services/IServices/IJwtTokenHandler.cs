using System.Security.Claims;

namespace BuyItPlatform.ListingsApi.Services.IServices
{
    public interface IJwtTokenHandler
    {
        ICollection<Claim> ExtractTokenData(string token);
    }
}
