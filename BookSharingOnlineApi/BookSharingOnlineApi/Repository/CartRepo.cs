using BookSharingOnlineApi.Models;
using BookSharingOnlineApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Repository
{
    public class CartRepo : ICartRepo
    {
        private readonly CartContext _context;

        public CartRepo(CartContext context)
        {
            _context = context;
        }

        public void Create(CartModel cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException();
            }
            _context.Carts.AddAsync(cart);
        }

        public async Task<IEnumerable<CartModel>> GetAll()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<IEnumerable<CartModel>> GetForUser(int userId)
        {
            List<CartModel> carts = await _context.Carts.ToListAsync();
            return carts.Where(c => c.UserId == userId).ToList();
        }

        public async Task<CartModel> GetCartOfUser(int cartId)
        {
            CartModel cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
            return cart;
        }

        public void Delete(CartModel cart)
        {
            _context.Carts.Remove(cart);
        }

        public async Task<bool> SaveChanges()
        {
            int rowsAffected = await _context.SaveChangesAsync();
            return (rowsAffected >= 0);
        }
    }
}