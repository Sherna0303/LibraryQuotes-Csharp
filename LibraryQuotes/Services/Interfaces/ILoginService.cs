using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Models.Persistence;

namespace LibraryQuotes.Services.Interfaces
{
    public interface ILoginService
    {
        Task<Users> GetUser(UserDTO user);
    }
}
