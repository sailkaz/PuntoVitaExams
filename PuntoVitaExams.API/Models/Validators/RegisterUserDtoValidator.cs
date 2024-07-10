using FluentValidation;
using PuntoVitaExams.API.DbContexts;

namespace PuntoVitaExams.API.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<UserDto>
    {
        public RegisterUserDtoValidator(ExamContext dbcontext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                   var emailInUse = dbcontext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "This email is taken");
                    }
                });
        }
    }
}
