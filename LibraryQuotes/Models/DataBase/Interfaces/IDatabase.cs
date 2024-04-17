using LibraryQuotes.Models.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryQuotes.Models.DataBase.Interfaces
{
    public interface IDatabase
    {
        DbSet<Copy> copy { get; set; }
        DbSet<Users> users { get; set; }
        Task<bool> SaveAsync();
    }
}
