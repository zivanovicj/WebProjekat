using FluentValidation;
using UserAdminAPI.DTO;

namespace UserAdminAPI.Validators
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
