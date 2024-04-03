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

        public ListCopiesEntity CalculateBudget(BudgetClientDTO payload)
        {
            var copies = _quoteListService.CalculatePriceListCopies(payload.ClientCopies);
            var listCopies = new List<CopyDTO>();
            var budget = new ListCopiesEntity();
            float totalBudget = payload.Budget;

            var bookMin = copies.Copies.Where(copy => copy is BookEntity).MinBy(copy => copy.Price);
            var novelMin = copies.Copies.Where(copy => copy is NovelEntity).MinBy(copy => copy.Price);
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

        private CopyDTO ConvertCopyToCopyDTO(CopyEntity copy, CopyType type)
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
