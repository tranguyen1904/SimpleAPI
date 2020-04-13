using Autofac;
using TestAPI.Contracts;
using TestAPI.Filters;
using TestAPI.Repositories;

namespace TestAPI
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //base.Load(builder);
            builder.RegisterType<RepositoryWrapper>().As<IRepositoryWrapper>().InstancePerLifetimeScope();
            builder.RegisterType<LoggerManager>().As<ILoggerManager>().SingleInstance();
            builder.RegisterType<ValidationFilterAttribute>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ValidateEntityExistsAttribute<>)).InstancePerLifetimeScope();
        }
    }
}
