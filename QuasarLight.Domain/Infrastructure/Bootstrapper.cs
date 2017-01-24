using System;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation;
using Incoding.Block.IoC;
using Incoding.CQRS;
using Incoding.Data;
using Incoding.EventBroker;
using Incoding.MvcContrib;
using StructureMap.Graph;

namespace QuasarLight.Domain.Infrastructure
{
    public class IncControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? base.GetControllerInstance(requestContext, null) : IoCFactory.Instance.Resolve<IController>(controllerType);
        }
    }

    public static class Bootstrapper
    {
        public static void Start()
        {
            MailSettings.ToEmail = WebConfigurationManager.AppSettings["ToEmail"];
            MailSettings.FromEmail = WebConfigurationManager.AppSettings["FromEmail"];
            MailSettings.FromName = WebConfigurationManager.AppSettings["FromName"];
            MailSettings.SubAccount = WebConfigurationManager.AppSettings["SubAccount"];
            MailSettings.MandrillApi = WebConfigurationManager.AppSettings["MandrillApi"];

            InitializeContainer();
        }

        public static void InitializeContainer()
        {
            ControllerBuilder.Current.SetControllerFactory(new IncControllerFactory());
            IoCFactory.Instance.Initialize(init => init.WithProvider(new StructureMapIoCProvider(r =>
            {
                //r.For<IDispatcher>().Use<DefaultDispatcher>();
                r.For<IEventBroker>().Use<DefaultEventBroker>();
                r.For<ITemplateFactory>().Singleton().Use<TemplateHandlebarsFactory>();

                //var configure = Fluently
                //       .Configure()
                //       .Database(MsSqlConfiguration.MsSql2012.ConnectionString(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
                //       .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Teacher.TeacherMap>())
                //       .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                //       .CurrentSessionContext<NhibernateSessionContext>()
                //       .BuildSessionFactory();
                //r.For<INhibernateSessionFactory>().Singleton().Use(() => new NhibernateSessionFactory(configure));
                r.For<IUnitOfWorkFactory>().Use<NhibernateUnitOfWorkFactory>();
                //r.For<IRepository>().Use<NhibernateRepository>();
                r.Scan(rg =>
                {
                    rg.TheCallingAssembly();
                    rg.WithDefaultConventions();
                    rg.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));
                    rg.ConnectImplementationsToTypesClosing(typeof(IEventSubscriber<>));
                    rg.AddAllTypesOf<ISetUp>();
                    rg.Assembly("QuasarLight.UI");
                    rg.Assembly("QuasarLight.Domain");
                });
            })));
        }
    }
}