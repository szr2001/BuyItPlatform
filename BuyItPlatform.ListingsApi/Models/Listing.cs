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
        public int SlotId { get; set; }
        [MaxLength(20)]
        public string Name { get; set; } = "";
        [MaxLength(200)]
        public string Description { get; set; } = "";
        [MaxLength(3)]
        //use AWS blob storage for storing Images and save the path in the db
        public ICollection<string> ImagePaths { get;set; } = new List<string>();
        public float Price { get; set; }
        public Currency Currency { get; set; }
        public string UserId { get; set; } = "";
        public TransactionType ListingType { get; set; }
        public Category Category { get; set; }
        public SubCategory? SubCategory { get; set; }
        public Color? Color { get; set; }
        [MaxLength(5)]
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
