using Autofac;
using Microsoft.Owin;

namespace QuasarLight.Common.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<UserContext>().As<IUserContext>();
            builder.RegisterType<OwinContext>().As<IOwinContext>();
        }
    }
}