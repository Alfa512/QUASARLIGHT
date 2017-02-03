
using Autofac;
using QuasarLight.Common.Contracts.Data;
using QuasarLight.Data.Entity;

namespace QuasarLight.Common.Modules
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().As<IDataContext>();
        }
    }
}