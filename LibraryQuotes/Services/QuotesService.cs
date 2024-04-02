using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Factories;

namespace LibraryQuotes.Services
{
    public class QuotesService : IQuotesService
    {
        private readonly ICopyFactory _copyFactory;

        public QuotesService(ICopyFactory copyFactory)
        {
            _copyFactory = copyFactory;
        }

        public Copy CalculatePrice(CopyDTO payload)
        {
            var copy = _copyFactory.Create(payload);
            copy.CalculateIncrease();
            return copy;
        }

        public ListCopies CalculatePriceListCopies(List<CopyDTO> payload)
        {
            var copies = payload.Select(item => _copyFactory.Create(item)).ToList();

            float total = 0;
            float discount = 0;

            foreach (var copy in copies)
            {
                total += copy.CalculateIncrease();
            }

            return new ListCopies(copies, total, discount);
        }
    }
}
