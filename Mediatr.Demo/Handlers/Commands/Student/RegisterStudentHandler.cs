using MediatR;
using Messages.Commands.Students;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers.Commands.Student
{
	public class RegisterStudentHandler
		: IRequestHandler<RegisterStudent, MediatrDemo.Domain.Student>
	{
		public Task<MediatrDemo.Domain.Student> Handle(RegisterStudent request, CancellationToken cancellationToken)
		{
			return Task.FromResult(
				new MediatrDemo.Domain.Student(
					request.Id,
					request.FirstName,
					request.MiddleName,
					request.LastName,
					request.BirthDate,
					request.Gender));
		}
	}
}
