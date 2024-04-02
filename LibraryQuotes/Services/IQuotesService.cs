using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Services
{
    public interface IQuotesService
    {
        Copy CalculatePrice(CopyDTO payload);
        ListCopies CalculatePriceListCopies(ClientDTO payload);
    }
}
