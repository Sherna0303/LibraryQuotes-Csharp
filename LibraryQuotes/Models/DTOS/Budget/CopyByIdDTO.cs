using FluentValidation;

namespace LibraryQuotes.Models.DTOS.Budget
{
    public class CopyByIdDTO
    {
        public int Id { get; set; }
    }

    public class CopyByIdValidator : AbstractValidator<CopyByIdDTO>
    {
        public CopyByIdValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
