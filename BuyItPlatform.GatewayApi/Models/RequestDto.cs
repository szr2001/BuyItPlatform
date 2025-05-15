using BuyItPlatform.GatewayApi.Enums;

namespace BuyItPlatform.GatewayApi.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        //used for FromBody requests for json
        public object BodyData { get; set; }
        //used for FromForm requests for binary
        public object FormData { get; set; }
        public string AccessToken { get; set; }
    }
}
