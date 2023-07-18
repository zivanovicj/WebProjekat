using FluentValidation;
using UserAdminAPI.DTO;

namespace UserAdminAPI.Validators
{
    public class GoogleLogInDTOValidator : AbstractValidator<GoogleLogInDTO>
    {
        public GoogleLogInDTOValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is required.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
        }
    }
}
