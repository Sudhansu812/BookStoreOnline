using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Models.Dto.CartDto
{
    public class CartDetailsDto
    {
        [Required]
        public int UserId { get; set; }
    }
}
