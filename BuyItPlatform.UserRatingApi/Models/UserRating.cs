using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BuyItPlatform.UserRatingApi.Models
{
    public class UserRating
    {
        //specify to auto increment ID
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(25)]
        [NotNull]
        public string TargetUserId { get; set; }

        [MaxLength(25)]
        [NotNull]
        public string UserId { get; set; }

        [Range(0,10)]
        [NotNull]
        public int Rating { get; set; }
    }
}
