using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Persistence;

namespace LibraryQuotes.Services.Interfaces
{
    public interface IQuotationService
    {
        Task<Copy> CalculatePrice(CopyDTO payload);
    }
}
