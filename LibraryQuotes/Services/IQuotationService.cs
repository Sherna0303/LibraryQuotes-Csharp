using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Services
{
    public interface IQuotationService
    {
        Copy CalculatePrice(ClientDTO payload);
    }
}
