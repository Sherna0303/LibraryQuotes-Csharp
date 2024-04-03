using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Enums;

namespace LibraryQuotes.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IQuoteListService _quoteListService;

        public BudgetService(IQuoteListService quoteListService)
        {
            _quoteListService = quoteListService;
        }

        public ListCopies CalculateBudget(BudgetClientDTO payload)
        {
            var copies = _quoteListService.CalculatePriceListCopies(payload.ClientCopies);
            var listCopies = new List<CopyDTO>();
            var budget = new ListCopies();
            float totalBudget = payload.Budget;

            var bookMin = copies.Copies.Where(copy => copy is Book).MinBy(copy => copy.Price);
            var novelMin = copies.Copies.Where(copy => copy is Novel).MinBy(copy => copy.Price);
            var book = ConvertCopyToCopyDTO(bookMin, CopyType.BOOK);
            var novel = ConvertCopyToCopyDTO(novelMin, CopyType.NOVEL);

            var (lowestPrice, highestPrice) = (bookMin.Price < novelMin.Price) ? (book, novel) : (novel, book);
            listCopies.Add(highestPrice);

            while (budget.Total <= totalBudget)
            {
                listCopies.Add(lowestPrice);
                payload.ClientCopies.Copies = listCopies;
                budget = _quoteListService.CalculatePriceListCopies(payload.ClientCopies);
            }

            listCopies.RemoveAt(listCopies.Count - 1);
            payload.ClientCopies.Copies = listCopies;
            budget = _quoteListService.CalculatePriceListCopies(payload.ClientCopies);

            if (listCopies.Count <= 10)
            {
                throw new ArgumentException("Budget entered is not enough for wholesale");
            }

            return budget;
        }

        private CopyDTO ConvertCopyToCopyDTO(Copy copy, CopyType type)
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
    }
}
