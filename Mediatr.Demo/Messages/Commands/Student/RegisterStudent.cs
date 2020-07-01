using FluentValidation;
using MediatR;
using MediatrDemo.Domain;
using System;

namespace Messages.Commands.Student
{
    public class RegisterStudent
        : IRequest<MediatrDemo.Domain.Student>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public Gender Gender { get; set; }
    }

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
