using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryQuotes.Repository.User
{
    public class UserRepository(IDatabase database) : IUserRepository
    {
        private readonly IDatabase _database = database;

        public async Task<Users?> VerifyAuthentication(string email, string password)
        {
            return await _database.users
                .SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public async Task<bool> EmailAlreadyRegistered(string email)
        {
            return await _database.users
                .AnyAsync(x => x.Email == email);
        }

        public async Task<Users?> AddUser(Users user)
        {
            await _database.users.AddAsync(user);
            return user;
        }

        public async Task<Users?> GetByEmail(string email)
        {
            return await _database.users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
