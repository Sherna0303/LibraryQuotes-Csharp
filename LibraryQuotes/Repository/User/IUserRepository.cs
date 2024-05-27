using LibraryQuotes.Models.Persistence;

namespace LibraryQuotes.Repository.User
{
    public interface IUserRepository
    {
        Task<Users?> VerifyAuthentication(string email, string password);
        Task<bool> EmailAlreadyRegistered(string email);
        Task<Users?> AddUser(Users user);
        Task<Users?> GetByEmail(string email);
    }
}
