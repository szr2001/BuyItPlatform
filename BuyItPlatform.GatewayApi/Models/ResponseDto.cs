namespace BuyItPlatform.GatewayApi.Models
{
    public class ResponseDto<T>
    {
        public T? Result { get; set; }
        public bool Success { get; set; } = false;
        public string? Message { get; set; }
    }
}
