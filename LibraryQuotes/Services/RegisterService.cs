using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Models.Persistence;
using LibraryQuotes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LibraryQuotes.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IDatabase _database;

        public RegisterService(IDatabase database)
        {
            _database = database;
        }

        public async Task<Users?> RegisterUser(UserRegisterDTO user)
        {
            bool AlreadyRegistered = await _database.users
                .AnyAsync(x => x.Email == user.Email); ;

            if (AlreadyRegistered)
            {
                return null;
            }


            var userDb = new Users()
            {
                Name = user.Name,
                Email = user.Email,
                Password = HashPassword(user.Password),
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            };

            await _database.users.AddAsync(userDb);

            if (!await _database.SaveAsync())
            {
                return null;
            }

            return userDb;
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
