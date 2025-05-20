using System.Net;

namespace BuyItPlatform.GatewayApi.Models
{
    //We use generics so it's able to deserialize the responseDto from the microservices,
    //if we just use an object type, then it would faill because it does't know in what to deserialize it.
    //because the result from the microservices can be different each time
    //and we need to serialize it back to a responseDTO, but we don't know
    //what type the Result is from the microservices.
    public class MicroserviceResponseDto<T>
    {
        public T? Result { get; set; }
        public bool Success { get; set; } = false;
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
