using System.Security.Claims;

namespace BuyItPlatform.CommentsApi.Services.IServices
{
    public interface IJwtTokenHandler
    {
        ICollection<Claim> ExtractTokenData(string token);
    }
}
