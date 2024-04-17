using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Models.Persistence;

namespace LibraryQuotes.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<Users?> RegisterUser(UserRegisterDTO user);
    }
}
