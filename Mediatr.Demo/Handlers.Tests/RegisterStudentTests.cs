using Autofac;
using FluentValidation;
using Handlers.Configuration;
using MediatR;
using Messages.Commands.Student;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Handlers.Tests
{
	public class RegisterStudentTests
	{
		[Fact]
		public async Task ShouldFailOnInvalidCommand()
		{
			ContainerBuilder builder = new ContainerBuilder();
			builder.AddMediatr();

			IContainer container = builder.Build();
			IMediator mediator = container.Resolve<IMediator>();

			await Assert.ThrowsAsync<ValidationException>(async () => await mediator.Send(new RegisterStudent()
			{
			}));
		}

		[Fact]
		public async Task ShouldSucceedOnValidCommand()
		{
			ContainerBuilder builder = new ContainerBuilder();
			builder.AddMediatr();

			IContainer container = builder.Build();
			IMediator mediator = container.Resolve<IMediator>();

			var command = new RegisterStudent()
			{
				Id = Guid.NewGuid(),
				FirstName = "first",
				LastName = "last",
				BirthDate = DateTime.UtcNow.AddYears(-30)
			};

			var student = await mediator.Send(command);

			Assert.Equal(command.FirstName, student.FirstName);
			Assert.Equal(command.LastName, student.LastName);
			Assert.Equal(command.BirthDate, student.BirthDate);
		}
	}
}