namespace BuyItPlatform.GatewayApi.Models
{
    public class ListingFIlterDto
    {
        public string? Name { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public string? Currency { get; set; }
        public string? ListingType { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public string? Color { get; set; }
        public ICollection<string> Tags { get; set; } = new List<string>();
    }
}
