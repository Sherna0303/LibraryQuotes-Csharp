using LibraryQuotes.Models.DTOS;

namespace LibraryQuotes.Services.Interfaces
{
    public interface IGetCopiesService
    {
        Task<ClientDTO> GetCopiesByIdAndAmountAsync(ClientListDTO payload);
        //Task<List<CopyDTO>> GetCopiesByIdAsync(BudgetClientDTO payload);
    }
}
