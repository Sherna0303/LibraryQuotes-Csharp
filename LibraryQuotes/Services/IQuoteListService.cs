using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Services
{
    public interface IQuoteListService
    {
        ListCopiesEntity CalculatePriceListCopies(ClientDTO payload);
    }
}
