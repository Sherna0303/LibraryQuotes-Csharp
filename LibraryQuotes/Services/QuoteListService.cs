using LibraryQuotes.Models.DTOS.QuoteList;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Factories;
using LibraryQuotes.Services.Interfaces;

namespace LibraryQuotes.Services
{
    public class QuoteListService : IQuoteListService
    {
        private readonly ICopyFactory _copyFactory;
        private readonly IGetCopiesService _getCopiesService;
        private readonly ICalculateSeniorityService _calculateSeniorityService;

        public QuoteListService(ICopyFactory copyFactory, IGetCopiesService getCopiesService, ICalculateSeniorityService calculateSeniorityService)
        {
            _copyFactory = copyFactory;
            _getCopiesService = getCopiesService;
            _calculateSeniorityService = calculateSeniorityService;
        }

        public ListCopiesEntity CalculatePriceListCopiesAndConvertToClientDTO(ClientListAndAmountDTO payload, string idUser)
        {
            var copiesDTO = _getCopiesService.GetCopiesByIdAndAmountAsync(payload).Result;

            if (copiesDTO == null)
            {
                throw new ArgumentException("The copy id does not exist in the database");
            }

            copiesDTO.AntiquityYears = _calculateSeniorityService.GetSeniority(idUser);

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
            payload.Sort((x, y) => y.Price.CompareTo(x.Price));

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

        private int CalculateAntiquityYears(DateOnly date)
{
    var today = DateTime.Now;
    var JoinDate = new DateTime(date.Year, date.Month, date.Day);
    TimeSpan totalDays = today - JoinDate;

    return totalDays.Days / 365;
}
    }
}
