using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Persistence;

namespace LibraryQuotes.Services.Interfaces
{
    public interface IQuotationService
    {
        Task<Copy> CalculatePrice(ClientDTO payload);
    }
}
