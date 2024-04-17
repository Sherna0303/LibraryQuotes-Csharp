using LibraryQuotes.Models.DTOS.User;

namespace LibraryQuotes.Services.Interfaces
{
    public interface ICreateToken
    {
        Task<string> GenerateToken(UserDTO user);
    }
}
