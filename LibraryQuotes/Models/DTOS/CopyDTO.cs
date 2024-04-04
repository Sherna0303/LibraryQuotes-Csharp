using FluentValidation;
using LibraryQuotes.Models.Enums;
using LibraryQuotes.Models.Persistence;

namespace LibraryQuotes.Models.DTOS
{
    public class CopyDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public float Price { get; set; }
        public CopyType Type { get; set; }
    }

    public class CopyValidator : AbstractValidator<CopyDTO>
    {
        public CopyValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Author).NotNull().NotEmpty();
            RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Type).NotNull().IsInEnum();
        }
    }
}
