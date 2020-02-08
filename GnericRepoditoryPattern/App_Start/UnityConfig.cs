using GenericRepositoryPatternServices.Interfaces;
using GenericRepositoryPatternServices.Repository;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace GenericRepositoryPatternWeb
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IRepository<IUnitOfWork>, Repository<IUnitOfWork>>();
            container.RegisterType<IGradeRepository, GradeRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}