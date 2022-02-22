using BookSharingOnlineApi.Models;
using BookSharingOnlineApi.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly OrderContext _context;

        public OrderRepo(OrderContext context)
        {
            _context = context;
        }

        public void Order(OrderModel order)
        {
            if (order == null)
            {
                throw new ArgumentNullException();
            }
            _context.Orders.AddAsync(order);
        }

        public async Task<bool> SaveChanges()
        {
            int rowsAffected = await _context.SaveChangesAsync();
            return (rowsAffected >= 0);
        }
    }
}