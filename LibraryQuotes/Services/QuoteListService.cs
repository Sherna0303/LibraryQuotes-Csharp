using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Factories;

namespace LibraryQuotes.Services
{
    public class QuoteListService : IQuoteListService
    {
        private readonly ICopyFactory _copyFactory;

        public QuoteListService(ICopyFactory copyFactory)
        {
            _copyFactory = copyFactory;
        }

        public ListCopies CalculatePriceListCopies(ClientDTO payload)
        {
            var copies = payload.Copies.Select(item => _copyFactory.Create(item)).ToList();

            float total = 0;
            float discount = 0;
            float RETAIL_INCREASE = ValidateIncreaseRetailPurchase(copies.Count);

            foreach (var copy in copies)
            {
                copy.CalculateIncrease(RETAIL_INCREASE);
            }

            if (copies.Count > 10)
            {
                discount = CalculateDiscounts(copies);
            }

            foreach (var copy in copies)
            {
                discount =+ copy.CalculateDiscount(payload.AntiquityYears);
                total += copy.TotalPrice;
            }

            return new ListCopies(payload.AntiquityYears, copies, total, discount);
        }

        private float CalculateDiscounts(List<Copy> payload)
        {
            payload = payload.OrderByDescending(x => x.Price).ToList();
            float discount = 0;

            for (int i = 10; i < payload.Count; i++)
            {
                discount += payload[i].CalculateWholesaleDiscount(payload.Count);
            }

            return discount;
        }

        private float ValidateIncreaseRetailPurchase(int count)
        {
            const float RETAIL_INCREASE = 1.02f;

            return count > 1 && count <= 10 ? RETAIL_INCREASE : 1;
        }
    }
}
