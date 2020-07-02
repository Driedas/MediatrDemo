using MediatR;
using MediatrDemo.Domain;
using System.Linq;

namespace Messages.Queries
{
	public class StudentsQuery
		: IRequest<IQueryable<Student>>
	{
	}
}
