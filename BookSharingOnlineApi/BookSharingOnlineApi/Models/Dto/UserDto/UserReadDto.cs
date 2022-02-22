using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models.Dto.UserDto
{
    public class UserReadDto
    {
        [Key]
        [Required]
        public int UserId { get; set; }

        [Required]
        public string UserFirstName { get; set; }

        [Required]
        public string UserLastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}