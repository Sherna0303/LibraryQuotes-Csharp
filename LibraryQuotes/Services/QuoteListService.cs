using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Factories;
using LibraryQuotes.Services.Interfaces;

namespace LibraryQuotes.Services
{
    public class QuoteListService : IQuoteListService
    {
        private readonly ICopyFactory _copyFactory;
        private readonly IGetCopiesService _getCopiesService;

        public QuoteListService(ICopyFactory copyFactory, IGetCopiesService getCopiesService)
        {
            _copyFactory = copyFactory;
            _getCopiesService = getCopiesService;
        }

        public ListCopiesEntity CalculatePriceListCopiesAndConvertToClientDTO(ClientListDTO payload)
        {
            var copiesDTO = _getCopiesService.GetCopiesByIdAndAmountAsync(payload).Result;

            if (copiesDTO == null)
            {
                return null;
            }

            return CalculatePriceListCopies(copiesDTO);
        }

        public ListCopiesEntity CalculatePriceListCopies(ClientDTO payload)
        {
            var copies = payload.Copies.Select(item => _copyFactory.Create(item)).ToList();

            float total = 0;
            float discount = 0;
            float RETAIL_INCREASE = ValidateIncreaseRetailPurchase(copies.Count);

            copies.ForEach(copy => copy.CalculateIncreaseDetal(RETAIL_INCREASE));

            if (copies.Count > 10)
            {
                CalculateDiscounts(copies);
            }

            foreach (var copy in copies)
            {
                copy.CalculateDiscount(payload.AntiquityYears);
                total += copy.TotalPrice;
                discount += copy.Discount;
            }

            return new ListCopiesEntity(payload.AntiquityYears, copies, total, discount);
        }

        private void CalculateDiscounts(List<CopyEntity> payload)
        {
            payload = payload.OrderByDescending(x => x.Price).ToList();

            for (int i = 10; i < payload.Count; i++)
            {
                payload[i].CalculateWholesaleDiscount(payload.Count);
            }
        }

        private float ValidateIncreaseRetailPurchase(int count)
        {
            const float RETAIL_INCREASE = 1.02f;

            return count > 1 && count <= 10 ? RETAIL_INCREASE : 1;
        }
    }
}
