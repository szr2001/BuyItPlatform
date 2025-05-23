using System.Security.Claims;

namespace BuyItPlatform.UserRatingApi.Service.IService
{
    public interface IJwtTokenHandler
    {
        ICollection<Claim> ExtractTokenData(string token);
    }
}
