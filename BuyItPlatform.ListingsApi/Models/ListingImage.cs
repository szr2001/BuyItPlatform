namespace BuyItPlatform.ListingsApi.Models
{
    public class ListingImage
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int ListingId { get; set; }
        public Listing ParentListing { get; set; }
    }
}
