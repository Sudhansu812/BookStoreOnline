using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Models.Dto.OrderDto
{
    public class OrderQuantity
    {
        int BookId { get; set; }
        int BookQuantity { get; set; }
    }
}
