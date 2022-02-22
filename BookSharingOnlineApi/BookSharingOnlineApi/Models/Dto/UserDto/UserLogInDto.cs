using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models.Dto.UserDto
{
    public class UserLogInDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}