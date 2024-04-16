using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Persistence;

namespace LibraryQuotes.Services.Interfaces
{
    public interface ILoginService
    {
        Task<Users> GetUser(UserDTO user);
    }
}
