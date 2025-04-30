namespace BuyItPlatform.GatewayApi.Models
{
    public class ResponseDto<T>
    {
        public T? Result { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
    }
}
