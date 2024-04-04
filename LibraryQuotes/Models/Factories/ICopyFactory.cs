using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Models.Factories
{
    public interface ICopyFactory
    {
        CopyEntity Create(CopyDTO payload);
    }
}
