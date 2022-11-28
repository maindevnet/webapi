using Autofac;
using Autofac.Integration.WebApi;
using QLKS.Data.EF;
using QLKS.Repository.IRepository;
using QLKS.Repository.Repository;
using QLKS.Service.Service;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace QlKS.WebApi.DI
{
    public class DependencyInjection
    {
        public static void DIRepository()
        {
            var builder = new ContainerBuilder();
            //Register WebApi Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());

            builder.RegisterType(typeof(QLKSEntities)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();


            IContainer container = builder.Build();
            //Set the WebApi DependencyResolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container); 
        }
    }
}