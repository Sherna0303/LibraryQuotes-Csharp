using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Models.Persistence;
using LibraryQuotes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryQuotes.Services
{
    public class LoginService : ILoginService
    {
        private readonly IDatabase _database;

        public LoginService(IDatabase database)
        {
            _database = database;
        }

        public async Task<Users?> GetUser(UserDTO user)
        {
            return await _database.users
                .SingleOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
        }
    }
}
