using FluentValidation;
using WebProjekat.DTO.UserDTO;

namespace WebProjekat.Validators.UserValidators
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
