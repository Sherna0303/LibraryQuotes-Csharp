using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Factories;
using System.ComponentModel.DataAnnotations;

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
            float RETAIL_INCREASE = 1;
            float WHOLESALE_DISCOUNT = 1;
            var copy = _copyFactory.Create(payload);
            copy.CalculateIncrease(RETAIL_INCREASE, WHOLESALE_DISCOUNT);

            return copy;
        }

        public ListCopies CalculatePriceListCopies(List<CopyDTO> payload)
        {
            var copies = payload.Select(item => _copyFactory.Create(item)).OrderByDescending(x => x.Price).ToList();

            float total = 0;
            float discount = 0;
            float RETAIL_INCREASE = ValidateIncreaseRetailPurchase(payload.Count);
            float WHOLESALE_DISCOUNT = ValidateWholesaleDiscount(payload.Count);

            for (int i = 0; i < copies.Count; i++)
            {
                total += (i >= 10) ? copies[i].CalculateIncrease(RETAIL_INCREASE, WHOLESALE_DISCOUNT) : copies[i].CalculateIncrease(RETAIL_INCREASE, 1);
            }


            return new ListCopies(copies, total, discount);
        }

        private float ValidateIncreaseRetailPurchase(int count)
        {
            const float RETAIL_INCREASE = 1.02f;

            return count > 1 && count <= 10 ? RETAIL_INCREASE : 1;
        }

        private float ValidateWholesaleDiscount(int count)
        {
            const float WHOLESALE_DISCOUNT = 0.15f;
            return 1 - (count * WHOLESALE_DISCOUNT / 100);
        }
    }
}
