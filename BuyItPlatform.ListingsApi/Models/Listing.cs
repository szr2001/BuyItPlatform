using BuyItPlatform.ListingsApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyItPlatform.ListingsApi.Models
{
    public class Listing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [MaxLength(3)]
        //use AWS blob storage for storing Images and save the path in the db
        public string[] ImagePaths { get;set; }
        public float Price { get; set; }
        [MaxLength(10)]
        public string Currency {get;set; }
        public int UserId { get; set; }
        public TransactionType ListingType { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public Color Color { get; set; }
        [MaxLength(5)]
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
