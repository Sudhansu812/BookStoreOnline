using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models.Dto.OrderDto
{
    public class OrderCreateDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int OrderQuantity { get; set; }
    }
}