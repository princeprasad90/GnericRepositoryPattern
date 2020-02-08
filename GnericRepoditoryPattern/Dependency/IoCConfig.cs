using Autofac;
using Autofac.Integration.Mvc;
using GenericRepositoryPatternServices.Interfaces;
using GenericRepositoryPatternServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace GenericRepositoryPatternWeb.Dependency
{
    public class IoCConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

          

            #region Register all controllers for the assembly
            // Note that ASP.NET MVC requests controllers by their concrete types, 
            // so registering them As<IController>() is incorrect. 
            // Also, if you register controllers manually and choose to specify 
            // lifetimes, you must register them as InstancePerDependency() or 
            // InstancePerHttpRequest() - ASP.NET MVC will throw an exception if 
            // you try to reuse a controller instance for multiple requests. 
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Register individual components
            builder.RegisterInstance(new GradeRepository()).As<IGradeRepository>();

            #endregion

            var container = builder.Build();
            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}