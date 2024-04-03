using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Services
{
    public interface IQuotationService
    {
        CopyEntity CalculatePrice(ClientDTO payload);
    }
}
