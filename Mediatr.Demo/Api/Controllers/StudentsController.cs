using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Results;
using MediatR;
using MediatrDemo.Domain;
using Messages.Commands.Students;
using Messages.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StudentsController : ControllerBase
	{
		private readonly IMediator mediator;

		public StudentsController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			IQueryable<Student> studentQuery = await this.mediator.Send(new StudentsQuery());
			Student student = studentQuery.Single(s => s.Id == id);

			return Ok(student);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterStudent command)
		{
			Student student = await this.mediator.Send(command);

			return Created($"/Students/{student.Id}", new ObjectIdResult(student.Id));
		}
	}
}
