using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models.Dto.BookDto
{
    public class BookCreateDto
    {
        [Required]
        public string BookTitle { get; set; }

        [Required]
        public string BookAuthorName { get; set; }

        [Required]
        public string BookDescription { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public double BookPrice { get; set; }

        [Required]
        public int BookQuantity { get; set; }

        [Required]
        public string BookCoverPath { get; set; }
    }
}