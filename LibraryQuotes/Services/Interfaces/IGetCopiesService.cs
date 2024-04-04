using LibraryQuotes.Models.DTOS;

namespace LibraryQuotes.Services.Interfaces
{
    public interface IGetCopiesService
    {
        Task<ClientDTO> GetCopiesByIdAndAmountAsync(ClientListAndAmountDTO payload);
        Task<ClientDTO> GetCopiesByIdAsync(ClientListIdDTO payload);
    }
}
