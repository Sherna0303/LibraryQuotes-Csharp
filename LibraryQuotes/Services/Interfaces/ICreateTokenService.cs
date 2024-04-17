using LibraryQuotes.Models.DTOS.User;

namespace LibraryQuotes.Services.Interfaces
{
    public interface ICreateTokenService
    {
        Task<string> GenerateToken(UserDTO user);
    }
}
