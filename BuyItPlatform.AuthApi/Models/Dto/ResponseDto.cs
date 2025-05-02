namespace BuyItPlatform.AuthApi.Models.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }
}
