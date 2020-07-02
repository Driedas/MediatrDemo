using FluentValidation;
using Messages.Commands.Students;

namespace Messages.Validation.Students
{
	public class RegisterStudentValidator
        : AbstractValidator<RegisterStudent>
    {
        public RegisterStudentValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.FirstName).NotNull();

            RuleFor(x => x.LastName).NotNull();

            RuleFor(x => x.BirthDate).NotEmpty();
        }
    }
}
