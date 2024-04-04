using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Services.Interfaces
{
    public interface IQuoteListService
    {
        ListCopiesEntity CalculatePriceListCopiesAndConvertToClientDTO(ClientListDTO payload);
        ListCopiesEntity CalculatePriceListCopies(ClientDTO payload);
    }
}
