using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Models.Persistence;
using LibraryQuotes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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
                .SingleOrDefaultAsync(x => x.Email == user.Email && x.Password == HashPassword(user.Password));
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
