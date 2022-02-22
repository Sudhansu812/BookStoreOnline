using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models.Dto.RatedBookDto
{
    public class RatedBookCreateDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}