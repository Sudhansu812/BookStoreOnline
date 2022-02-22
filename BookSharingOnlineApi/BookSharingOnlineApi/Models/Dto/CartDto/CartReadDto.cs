using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models.Dto.CartDto
{
    public class CartReadDto
    {
        [Key]
        [Required]
        public int CartId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int CartQuantity { get; set; }

        [Required]
        public double BookPrice { get; set; }

        [Required]
        public double CartSumTotal { get; set; }
    }
}