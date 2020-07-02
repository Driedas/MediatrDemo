using Autofac;
using FluentValidation;
using Handlers.Behaviors;
using Handlers.Configuration;
using MediatR;
using Messages.Commands.Students;
using Messages.Security;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Handlers.Tests
{
	public class RegisterStudentTests
	{
		//[Fact]
		//public async Task MediatorShouldExecuteAllPipelineBehaviors()
		//{
		//	IMediator mediator = GetMediator(builder =>
		//	{
		//		new Mock( typeof(LoggingBehavior<,>));

		//		builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
		//		builder.RegisterGeneric(typeof(AuthorizationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
		//		builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
		//		builder.RegisterGeneric(typeof(TransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>));
		//	});

		//	await mediator.Send(GetValidCommand());
		//}

		[Fact]
		public async Task ShouldFailOnInvalidCommand()
		{
			IMediator mediator = GetMediator();

			await Assert.ThrowsAsync<ValidationException>(async () => await mediator.Send(GetInvalidCommand()));
		}

		[Fact]
		public async Task ShouldSucceedOnValidCommand()
		{
			IMediator mediator = GetMediator();

			RegisterStudent command = GetValidCommand();

			var student = await mediator.Send(command);

			Assert.Equal(command.Id, student.Id);
			Assert.Equal(command.FirstName, student.FirstName);
			Assert.Equal(command.LastName, student.LastName);
			Assert.Equal(command.BirthDate, student.BirthDate);
		}

		[Fact]
		public async Task UnauthorizedRequestShouldThrowUnauthorizedAccessException()
		{
			IMediator mediator = GetMediator(builder =>
			{
				var authService = new Mock<IAuthenticationService>();
				authService.Setup(x => x.HasPermission(It.IsAny<ClaimsPrincipal>(), It.IsAny<ApplicationPermission>()))
					.Returns(false);

				builder.Register<IAuthenticationService>(c => authService.Object);
			});

			RegisterStudent command = GetValidCommand();

			await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await mediator.Send(command));
		}

		private IMediator GetMediator(Action<ContainerBuilder> containerCustomization = null)
		{
			ContainerBuilder builder = new ContainerBuilder();
			builder.AddMediatr();
			builder.AddAuthentication();

			ClaimsIdentity identity = new ClaimsIdentity();
			identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
			ClaimsPrincipal principal = new ClaimsPrincipal(identity);
			builder.Register<ClaimsPrincipal>(c => principal);

			containerCustomization?.Invoke(builder);

			IContainer container = builder.Build();
			IMediator mediator = container.Resolve<IMediator>();

			return mediator;
		}

		private RegisterStudent GetInvalidCommand()
		{
			return new RegisterStudent()
			{
			};
		}

		private RegisterStudent GetValidCommand()
		{
			return new RegisterStudent()
			{
				Id = Guid.NewGuid(),
				FirstName = "first",
				LastName = "last",
				BirthDate = DateTime.UtcNow.AddYears(-30)
			};
		}
	}
}