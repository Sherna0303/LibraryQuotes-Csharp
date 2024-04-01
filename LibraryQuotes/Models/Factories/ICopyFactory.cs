using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Models.Factories
{
    public interface ICopyFactory
    {
        Copy Create(CopyDTO payload);
    }
}
