using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Models.Dto.CartDto
{
    public class CartTableDto
    {
        public int CartId { get; set; }
        public string BookCoverPath { get; set; }
        public string BookTitle { get; set; }
        public int BookQuantity { get; set; }
        public double BookPrice { get; set; }
        public double BookSumTotal { get; set; }
    }
}
