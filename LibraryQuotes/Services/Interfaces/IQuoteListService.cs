using LibraryQuotes.Models.DTOS.QuoteList;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Services.Interfaces
{
    public interface IQuoteListService
    {
        ListCopiesEntity CalculatePriceListCopiesAndConvertToClientDTO(ClientListAndAmountDTO payload);
        ListCopiesEntity CalculatePriceListCopies(ClientDTO payload);
    }
}
