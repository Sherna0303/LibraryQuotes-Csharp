using LibraryQuotes.Models.DTOS.Budget;
using LibraryQuotes.Models.DTOS.QuoteList;

namespace LibraryQuotes.Services.Interfaces
{
    public interface IGetCopiesService
    {
        Task<ClientDTO> GetCopiesByIdAndAmountAsync(ClientListAndAmountDTO payload);
        Task<ClientDTO> GetCopiesByIdAsync(ClientListIdDTO payload);
    }
}
