using LibraryQuotes.Models.DTOS.User;

namespace LibraryQuotes.Services.Interfaces
{
    public interface ICreateToken
    {
        string GenerateToken(UserDTO user);
    }
}
