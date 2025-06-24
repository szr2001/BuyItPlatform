using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BuyItPlatform.UserRatingApi.Models
{
    public class UserRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(80)]
        [NotNull]
        public string TargetUserId { get; set; }

        [MaxLength(80)]
        [NotNull]
        public string UserId { get; set; }

        [Range(0,10)]
        [NotNull]
        public int Rating { get; set; }
    }
}
