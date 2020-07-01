using Autofac;
using FluentValidation;
using Handlers.Behaviors;
using Handlers.Commands.Student;
using MediatR;
using MediatR.Pipeline;
using Messages.Commands.Student;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handlers.Configuration
{
	public static class MediatrExtensions
	{
		public static void AddMediatr(this ContainerBuilder builder)
		{
			builder.RegisterType<Mediator>()
				.As<IMediator>()
				.InstancePerLifetimeScope();

			builder.Register<ServiceFactory>(context =>
			{
				var c = context.Resolve<IComponentContext>();
				return t => c.Resolve(t);
			});

			builder.RegisterAssemblyTypes(typeof(Root).Assembly)
				.AsClosedTypesOf(typeof(IRequestHandler<,>));

			builder.RegisterAssemblyTypes(typeof(Root).Assembly)
				.AsClosedTypesOf(typeof(IRequestExceptionHandler<,,>));

			builder.RegisterAssemblyTypes(typeof(Root).Assembly)
				.AsClosedTypesOf(typeof(IRequestExceptionAction<>));

			builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
			builder.RegisterGeneric(typeof(AuthorizationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
			builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
			builder.RegisterGeneric(typeof(TransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>));

			builder.RegisterAssemblyTypes(typeof(RegisterStudentValidator).Assembly)
				.AsClosedTypesOf(typeof(IValidator<>));
		}
	}
}