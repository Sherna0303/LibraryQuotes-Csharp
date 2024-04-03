using FluentValidation;

namespace LibraryQuotes.Models.DTOS
{
    public class BudgetClientDTO
    {
        public float Budget { get; set; }
        public ClientDTO ClientCopies { get; set; }
    }

    public class BudgetClientValidator : AbstractValidator<BudgetClientDTO>
    {
        public BudgetClientValidator()
        {
            RuleFor(x => x.Budget).NotNull().NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.ClientCopies).NotNull().NotEmpty();
        }
    }
}
