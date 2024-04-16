using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Models.Persistence;
using LibraryQuotes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                Password = user.Password,
                AntiquityYears = DateOnly.FromDateTime(DateTime.Now)
            };

            await _database.users.AddAsync(userDb);

            if (!await _database.SaveAsync())
            {
                return null;
            }

            return userDb;
        }
    }
}
