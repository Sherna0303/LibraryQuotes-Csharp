using FluentValidation;

namespace LibraryQuotes.Models.DTOS
{
    public class ClientDTO
    {
        public int AntiquityYears { get; set; }
        public List<CopyDTO> Copies { get; set; }
    }

    public class ClientValidator : AbstractValidator<ClientDTO>
    {
        public ClientValidator()
        {
            RuleFor(x => x.AntiquityYears).NotNull().GreaterThanOrEqualTo(0);
            RuleForEach(x => x.Copies).SetValidator(new CopyValidator());
        }
    }
}
