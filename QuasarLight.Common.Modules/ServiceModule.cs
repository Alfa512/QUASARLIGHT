using Autofac;
using QuasarLight.Business.Services;
using QuasarLight.Common.Contracts.Services;

namespace QuasarLight.Common.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<CryptoService>().As<ICrypto>();
            builder.RegisterType<MailService>().As<IMailService>();
            builder.RegisterType<TemplateService>().As<ITemplateService>();
        }
    }
}