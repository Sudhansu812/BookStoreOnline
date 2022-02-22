using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models
{
    public class CartModel
    {
        [Key]
        [Required]
        public int CartId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CartQuantity { get; set; }

        [Required]
        public double BookPrice { get; set; }

        [Required]
        public double CartSumTotal { get; set; }
    }
}