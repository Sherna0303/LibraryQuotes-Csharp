using FluentValidation;

namespace LibraryQuotes.Models.DTOS.User
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
