using FluentValidation;

namespace LibraryQuotes.Models.DTOS.Budget
{
    public class ClientListIdDTO
    {
        public List<CopyByIdDTO> Copies { get; set; }
    }

    public class ClientListIdValidator : AbstractValidator<ClientListIdDTO>
    {
        public ClientListIdValidator()
        {
            RuleFor(x => x.Copies).NotNull().NotEmpty();
            RuleForEach(x => x.Copies).SetValidator(new CopyByIdValidator());
        }
    }
}
