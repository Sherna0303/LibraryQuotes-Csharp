using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Factories;

namespace LibraryQuotes.Services
{
    public class QuotationService : IQuotationService
    {
        private readonly ICopyFactory _copyFactory;

        public QuotationService(ICopyFactory copyFactory)
        {
            _copyFactory = copyFactory;
        }

        public CopyEntity CalculatePrice(ClientDTO payload)
        {
            float RETAIL_INCREASE = 1;
            var copy = _copyFactory.Create(payload.Copies.FirstOrDefault());
            copy.CalculateIncrease(RETAIL_INCREASE);
            copy.CalculateDiscount(payload.AntiquityYears);

            return copy;
        }
    }
}
