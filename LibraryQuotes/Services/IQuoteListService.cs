using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Services
{
    public interface IQuoteListService
    {
        ListCopies CalculatePriceListCopies(ClientDTO payload);
    }
}
