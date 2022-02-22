using BookSharingOnlineApi.Models;
using BookSharingOnlineApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly UserContext _context;

        public UserRepo(UserContext context)
        {
            _context = context;
        }

        public void Register(UserModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            _context.Users.AddAsync(user);
        }

        public async Task<UserModel> LogIn(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));
        }

        public async Task<bool> SaveChanges()
        {
            int rowsAffected = await _context.SaveChangesAsync();
            return (rowsAffected >= 0);
        }
    }
}