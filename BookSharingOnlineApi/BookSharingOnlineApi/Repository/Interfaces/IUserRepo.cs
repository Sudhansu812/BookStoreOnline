using BookSharingOnlineApi.Models;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Repository.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> SaveChanges();

        Task<UserModel> LogIn(string email);

        void Register(UserModel user);
    }
}