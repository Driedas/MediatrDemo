using MediatR;
using MediatrDemo.Domain;
using Messages.Commands.Students;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers.Commands.Students
{
	public class RegisterStudentHandler
		: IRequestHandler<RegisterStudent, Student>
	{
		public Task<Student> Handle(RegisterStudent request, CancellationToken cancellationToken)
		{
			return Task.FromResult(
				new Student(
					request.Id,
					request.FirstName,
					request.MiddleName,
					request.LastName,
					request.BirthDate,
					request.Gender));
		}
	}
}
