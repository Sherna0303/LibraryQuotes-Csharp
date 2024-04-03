using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Services
{
    public interface IBudgetService
    {
        ListCopiesEntity CalculateBudget(BudgetClientDTO payload);
    }
}
