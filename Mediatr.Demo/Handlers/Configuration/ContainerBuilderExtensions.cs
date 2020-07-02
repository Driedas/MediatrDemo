using Autofac;

namespace Handlers.Configuration
{
	public static class ContainerBuilderExtensions
	{
		public static void AddMediatr(this ContainerBuilder builder)
		{
			builder.RegisterModule<MediatrModule>();
		}

		public static void AddAuthentication(this ContainerBuilder builder)
		{
			builder.RegisterModule<SecurityModule>();
		}
	}
}
