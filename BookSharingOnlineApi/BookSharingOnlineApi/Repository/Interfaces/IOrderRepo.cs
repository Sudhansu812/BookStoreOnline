using BookSharingOnlineApi.Models;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Repository.Interfaces
{
    public interface IOrderRepo
    {
        Task<bool> SaveChanges();

        void Order(OrderModel Order);
    }
}