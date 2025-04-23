using BuyItPlatform.GatewayApi.Services.IServices;
using BuyItPlatform.GatewayApi.Utility;

namespace BuyItPlatform.GatewayApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApiCallsService serviceBase;
        private readonly MicroservicesUrls microservicesUrl;
        public AuthService(IApiCallsService serviceBase, MicroservicesUrls microservicesUrl)
        {
            this.serviceBase = serviceBase;
            this.microservicesUrl = microservicesUrl;
        }
    }
}
