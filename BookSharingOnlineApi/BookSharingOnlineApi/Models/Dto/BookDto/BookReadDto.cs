using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models.Dto.BookDto
{
    public class BookReadDto
    {
        [Key]
        [Required]
        public int BookId { get; set; }

        [Required]
        public string BookTitle { get; set; }

        [Required]
        public string BookAuthorName { get; set; }

        [Required]
        public string BookDescription { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public double BookRating { get; set; }

        [Required]
        public int BookNumberOfRatings { get; set; }

        [Required]
        public double BookPrice { get; set; }

        [Required]
        public string BookCoverPath { get; set; }
    }
}