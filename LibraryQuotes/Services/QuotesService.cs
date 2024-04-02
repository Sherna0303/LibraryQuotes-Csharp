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
            float RETAIL_INCREASE = 1;
            var copy = _copyFactory.Create(payload);
            copy.CalculateIncrease(RETAIL_INCREASE);

            return copy;
        }

        public ListCopies CalculatePriceListCopies(ClientDTO payload)
        {
            var copies = payload.Copies.Select(item => _copyFactory.Create(item)).ToList();

            float total = 0;
            float discount = 0;
            float RETAIL_INCREASE = ValidateIncreaseRetailPurchase(copies.Count);
            float WHOLESALE_DISCOUNT = ValidateWholesaleDiscount(copies.Count);
            float ANTIQUITY_DISCOUNT = ValidateAntiquityDiscount(payload.AntiquityYears);

            for (int i = 0; i < copies.Count; i++)
            {
                float copyPrice = copies[i].CalculateIncrease(RETAIL_INCREASE);
                total += copyPrice;

                if (i >= 10)
                {

                    discount += copyPrice * WHOLESALE_DISCOUNT;
                }
            }

            discount += total * ANTIQUITY_DISCOUNT;
            total -= discount;

            return new ListCopies(payload.AntiquityYears, copies, total, discount);
        }

        private float ValidateIncreaseRetailPurchase(int count)
        {
            const float RETAIL_INCREASE = 1.02f;

            return count > 1 && count <= 10 ? RETAIL_INCREASE : 1;
        }

        private float ValidateWholesaleDiscount(int count)
        {
            const float WHOLESALE_DISCOUNT = 0.15f;
            return count * WHOLESALE_DISCOUNT / 100;
        }

        private float ValidateAntiquityDiscount(int years)
        {
            float DISCOUNT;
            if (years <= 0)
            {
                DISCOUNT = 0;
            }
            else if (years >= 1 && years <= 2)
            {
                DISCOUNT = 0.12f;
            }
            else
            {
                DISCOUNT = 0.17f;
            }

            return DISCOUNT;
        }
    }
}
