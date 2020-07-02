using MediatR;
using MediatrDemo.Domain;
using Messages.Queries;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers.Queries.Students
{
	public class StudentsQueryHandler
		: IRequestHandler<StudentsQuery, IQueryable<Student>>
	{
		public async Task<IQueryable<Student>> Handle(StudentsQuery request, CancellationToken cancellationToken)
		{
			return new Student[]
			{
				new Student(
					Guid.Parse("0f419516-f867-4941-af32-5678279a6349"),
					"first",
					null,
					"last",
					DateTimeOffset.UtcNow.AddYears(-30),
					Gender.Unknown)
			}.AsQueryable();
		}
	}
}
