using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models
{
    public class RatedBookModel
    {
        [Key]
        [Required]
        public int RatingId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}