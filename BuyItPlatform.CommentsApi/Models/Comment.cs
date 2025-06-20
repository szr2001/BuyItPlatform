using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuyItPlatform.CommentsApi.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(200)]
        public string Content { get; set; }
        [MaxLength(38)]
        public string ListingId { get; set; }
        [MaxLength(38)]
        public string UserId { get; set; }
    }
}
