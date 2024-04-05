﻿using LibraryQuotes.Models.DTOS.Budget;
using LibraryQuotes.Models.DTOS.QuoteList;
using LibraryQuotes.Models.Entities;

namespace LibraryQuotes.Services.Interfaces
{
    public interface IBudgetService
    {
        ListCopiesEntity CalculateBudgetAndConvertToClientDTO(BudgetClientDTO payload);
        ListCopiesEntity CalculateBudget(ClientDTO payload, float budget);
    }
}
