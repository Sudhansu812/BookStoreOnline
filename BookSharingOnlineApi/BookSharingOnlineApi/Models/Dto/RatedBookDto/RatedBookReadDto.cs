using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models.Dto.RatedBookDto
{
    public class RatedBookReadDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }
    }
}