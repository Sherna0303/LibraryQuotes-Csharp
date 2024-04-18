using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Models.Persistence;
using LibraryQuotes.Repository.User;
using LibraryQuotes.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace LibraryQuotes.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Users?> GetUser(UserDTO user)
        {
            return await _userRepository.VerifyAuthentication(user.Email, HashPassword(user.Password));
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
