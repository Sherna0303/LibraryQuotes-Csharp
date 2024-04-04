using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Services.Interfaces
{
    public interface IQuoteListService
    {
        ListCopiesEntity CalculatePriceListCopies(ClientDTO payload);
    }
}
