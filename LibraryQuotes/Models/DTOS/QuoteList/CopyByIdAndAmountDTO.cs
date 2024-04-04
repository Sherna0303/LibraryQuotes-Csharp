using FluentValidation;

namespace LibraryQuotes.Models.DTOS.QuoteList
{
    public class CopyByIdAndAmountDTO
    {
        public int Id { get; set; }
        public int Amount { get; set; }
    }

    public class CopyByIdAndAmountValidator : AbstractValidator<CopyByIdAndAmountDTO>
    {
        public CopyByIdAndAmountValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(x => x.Amount).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
