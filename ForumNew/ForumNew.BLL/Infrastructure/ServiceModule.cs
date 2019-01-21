using Autofac;
using ForumNew.DAL.Interfaces;
using ForumNew.DAL.Repositories;

namespace ForumNew.BLL.Infrastructure
{
    public class ServiceModule : Module
    {
        public ServiceModule(string connection, ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().WithParameter("connectionString", connection);
        }
    }
}
