using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models
{
    public class OrderModel
    {
        [Key]
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int OrderQuantity { get; set; }

        [Required]
        public double BookPrice { get; set; }

        [Required]
        public double OrderSumTotal { get; set; }
    }
}