using LibraryQuotes.Models.DTOS.Budget;
using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.DTOS.QuoteList;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Enums;
using LibraryQuotes.Services.Interfaces;

namespace LibraryQuotes.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IQuoteListService _quoteListService;
        private readonly IGetCopiesService _getCopiesService;
        private readonly ICalculateSeniorityService _calculateSeniorityService;

        public BudgetService(IQuoteListService quoteListService, IGetCopiesService getCopiesService, ICalculateSeniorityService calculateSeniorityService)
        {
            _quoteListService = quoteListService;
            _getCopiesService = getCopiesService;
            _calculateSeniorityService = calculateSeniorityService;
        }

        public ListCopiesEntity CalculateBudgetAndConvertToClientDTO(BudgetClientDTO payload, string idUser)
        {
            var copiesDTO = _getCopiesService.GetCopiesByIdAsync(payload.ClientCopies).Result;

            if (copiesDTO == null)
            {
                throw new ArgumentException("The copy id does not exist in the database");
            }

            copiesDTO.AntiquityYears = _calculateSeniorityService.GetSeniority(idUser);

            return CalculateBudget(copiesDTO, payload.Budget);
        }

        public ListCopiesEntity CalculateBudget(ClientDTO payload, float budget)
        {
            var copies = _quoteListService.CalculatePriceListCopies(payload);
            var budgetTotal = new ListCopiesEntity();
            var (lowestPriceCopy, highestPriceCopy) = GetLowestAndHighestPriceCopies(copies);
            var listCopies = new List<CopyDTO> { highestPriceCopy };

            while (budgetTotal.Total <= budget && listCopies.Count <= 700)
            {
                listCopies.Add(lowestPriceCopy);
                payload.Copies = listCopies;
                budgetTotal = _quoteListService.CalculatePriceListCopies(payload);
            }

            listCopies.RemoveAt(listCopies.Count - 1);
            payload.Copies = listCopies;
            budgetTotal = _quoteListService.CalculatePriceListCopies(payload);

            if (listCopies.Count <= 10)
            {
                throw new ArgumentException("Budget entered is not enough for wholesale");
            }

            return budgetTotal;
        }

        private (CopyDTO lowestPriceCopy, CopyDTO highestPriceCopy) GetLowestAndHighestPriceCopies(ListCopiesEntity copies)
        {
            var book = ConvertCopyToCopyDTO(copies.Copies.Where(copy => copy is BookEntity).MinBy(copy => copy.Price), CopyType.BOOK);
            var novel = ConvertCopyToCopyDTO(copies.Copies.Where(copy => copy is NovelEntity).MinBy(copy => copy.Price), CopyType.NOVEL);

            return book.Price < novel.Price ? (book, novel) : (novel, book);
        }

        private CopyDTO ConvertCopyToCopyDTO(CopyEntity copy, CopyType type)
        {
            if (copy != null)
            {
                var copyDto = new CopyDTO
                {
                    Name = copy.Name,
                    Author = copy.Author,
                    Price = copy.Price,
                    Type = (type == CopyType.BOOK) ? CopyType.BOOK : CopyType.NOVEL
                };
                return copyDto;
            }

            throw new ArgumentException("A list was sent with only one type of copy");
        }
    }
}
