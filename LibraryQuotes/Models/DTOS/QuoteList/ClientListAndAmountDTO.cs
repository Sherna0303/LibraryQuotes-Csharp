using FluentValidation;

namespace LibraryQuotes.Models.DTOS.QuoteList
{
    public class ClientListAndAmountDTO
    {
        public List<CopyByIdAndAmountDTO> Copies { get; set; }
    }

    public class ClientListAndAmountValidator : AbstractValidator<ClientListAndAmountDTO>
    {
        public ClientListAndAmountValidator()
        {
            RuleFor(x => x.Copies).NotNull().NotEmpty();
            RuleForEach(x => x.Copies).SetValidator(new CopyByIdAndAmountValidator());
        }
    }
}
