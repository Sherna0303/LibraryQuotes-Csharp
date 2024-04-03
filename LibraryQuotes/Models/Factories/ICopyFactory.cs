using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Models.Factories
{
    public interface ICopyFactory
    {
        CopyEntity Create(CopyDTO payload);
    }
}
