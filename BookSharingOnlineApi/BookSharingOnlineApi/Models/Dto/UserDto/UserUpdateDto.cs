using System.ComponentModel.DataAnnotations;

namespace BookSharingOnlineApi.Models.Dto.UserDto
{
    public class UserUpdateDto
    {
        [Required]
        public string UserFirstName { get; set; }

        [Required]
        public string UserLastName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}