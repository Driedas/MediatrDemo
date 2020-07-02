using Autofac;
using FluentValidation;
using Handlers.Behaviors;
using MediatR;
using MediatR.Pipeline;

namespace Handlers.Configuration
{
	public class MediatrModule
		: Module
	{
		protected override void Load(ContainerBuilder builder)
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

			builder.RegisterAssemblyTypes(typeof(Messages.Root).Assembly)
				.AsClosedTypesOf(typeof(IValidator<>));
		}
	}
}
