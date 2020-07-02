using System;
using MediatR;
using MediatrDemo.Domain;
using Messages.Security;

namespace Messages.Commands.Students
{
    [RequirePermission(ApplicationPermission.RegisterStudent)]
    public class RegisterStudent
        : IRequest<Student>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}
