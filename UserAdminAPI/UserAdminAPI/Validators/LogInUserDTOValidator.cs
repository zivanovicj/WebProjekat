using FluentValidation;
using UserAdminAPI.DTO;

namespace UserAdminAPI.Validators
{
    public class LogInUserDTOValidator : AbstractValidator<LogInUserDTO>
    {
        public LogInUserDTOValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
