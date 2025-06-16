namespace BuyItPlatform.ListingsApi.Models.Dto
{
    public class ListingViewDto
    {
        public int Id { get; set; }
        public int SlotId { get; set; }
        public string UserId { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public ICollection<string> ImagePaths { get; set; } = new List<string>();
        public float Price { get; set; }
        public string Currency { get; set; } = "";
        public string ListingType { get; set; } = "";
        public string Category { get; set; } = "";
        public string? SubCategory { get; set; }
        public string? Color { get; set; }
        public ICollection<string> Tags { get; set; } = new List<string>();
    }
}
