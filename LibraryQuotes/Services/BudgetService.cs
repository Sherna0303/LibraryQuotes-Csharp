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

        public BudgetService(IQuoteListService quoteListService, IGetCopiesService getCopiesService)
        {
            _quoteListService = quoteListService;
            _getCopiesService = getCopiesService;
        }

        public ListCopiesEntity CalculateBudgetAndConvertToClientDTO(BudgetClientDTO payload)
        {
            var copiesDTO = _getCopiesService.GetCopiesByIdAsync(payload.ClientCopies).Result;

            if (copiesDTO == null)
            {
                throw new ArgumentException("The copy id does not exist in the database");
            }

            return CalculateBudget(copiesDTO, payload.Budget);
        }

        public ListCopiesEntity CalculateBudget(ClientDTO payload, float budget)
        {
            var copies = _quoteListService.CalculatePriceListCopies(payload);
            var listCopies = new List<CopyDTO>();
            var budgetTotal = new ListCopiesEntity();

            var bookMin = copies.Copies.Where(copy => copy is BookEntity).MinBy(copy => copy.Price);
            var novelMin = copies.Copies.Where(copy => copy is NovelEntity).MinBy(copy => copy.Price);
            var book = ConvertCopyToCopyDTO(bookMin, CopyType.BOOK);
            var novel = ConvertCopyToCopyDTO(novelMin, CopyType.NOVEL);

            var (lowestPrice, highestPrice) = (bookMin.Price < novelMin.Price) ? (book, novel) : (novel, book);
            listCopies.Add(highestPrice);

            while (budgetTotal.Total <= budget)
            {
                listCopies.Add(lowestPrice);
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
