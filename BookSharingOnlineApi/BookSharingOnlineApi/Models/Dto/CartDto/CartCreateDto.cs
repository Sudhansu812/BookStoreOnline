using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models.Dto.CartDto
{
    public class CartCreateDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CartQuantity { get; set; }
    }
}