using BookSharingOnlineApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Repository.Interfaces
{
    public interface ICartRepo
    {
        Task<bool> SaveChanges();

        Task<IEnumerable<CartModel>> GetAll();

        Task<IEnumerable<CartModel>> GetForUser(int userId);

        Task<CartModel> GetCartOfUser(int userId);

        void Delete(CartModel cart);

        void Create(CartModel cart);
    }
}