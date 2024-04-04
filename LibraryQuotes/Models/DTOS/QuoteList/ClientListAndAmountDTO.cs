using FluentValidation;

namespace LibraryQuotes.Models.DTOS.QuoteList
{
    public class ClientListAndAmountDTO
    {
        public int AntiquityYears { get; set; }
        public List<CopyByIdAndAmountDTO> Copies { get; set; }
    }

    public class ClientListAndAmountValidator : AbstractValidator<ClientListAndAmountDTO>
    {
        public ClientListAndAmountValidator()
        {
            RuleFor(x => x.AntiquityYears).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Copies).NotNull().NotEmpty();
            RuleForEach(x => x.Copies).SetValidator(new CopyByIdAndAmountValidator());
        }
    }
}
