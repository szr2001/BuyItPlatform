using BuyItPlatform.AuthApi.Models;

namespace BuyItPlatform.AuthApi.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(BuyItUser user);
    }
}
