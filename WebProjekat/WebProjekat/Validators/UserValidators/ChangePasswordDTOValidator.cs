using FluentValidation;
using WebProjekat.DTO.UserDTO;

namespace WebProjekat.Validators.UserValidators
{
    public class ChangePasswordDTOValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordDTOValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.OldPassword).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty();
            RuleFor(x => x.NewPasswordConfirm).NotEmpty();
        }
    }
}
