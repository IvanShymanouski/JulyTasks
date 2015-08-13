using System.Data.Entity;
using Ninject.Web.Common;
using Ninject;
using ORM;
using ORM.Interfaces;

namespace CustomNinjectDependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            Database.SetInitializer<EntityModel>(new InitializeEntityModel());
            kernel.Bind<DbContext>().To<EntityModel>().InRequestScope();

            kernel.Bind<IUserRepository>().To<UserRepository>();

        }

        public static void Reconficuration(this IKernel kernel)
        {
            ((EntityModel)kernel.GetService(typeof(DbContext))).Dispose();
        }
    }

}
