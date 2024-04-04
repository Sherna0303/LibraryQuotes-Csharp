using FluentValidation;

namespace LibraryQuotes.Models.DTOS.Budget
{
    public class ClientListIdDTO
    {
        public int AntiquityYears { get; set; }
        public List<CopyByIdDTO> Copies { get; set; }
    }

    public class ClientListIdValidator : AbstractValidator<ClientListIdDTO>
    {
        public ClientListIdValidator()
        {
            RuleFor(x => x.AntiquityYears).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Copies).NotNull().NotEmpty();
            RuleForEach(x => x.Copies).SetValidator(new CopyByIdValidator());
        }
    }
}
