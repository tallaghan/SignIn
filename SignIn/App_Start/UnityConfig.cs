using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using SignIn.Repositories;
using SignIn.Services;

namespace SignIn
{
    /// <summary>
    /// configure what concrete classes should be instantiated
    /// </summary>
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<ISigningsRepository, SigningsRepository>();
            container.RegisterType<EmployeeService, EmployeeService>();
            container.RegisterType<SigningsService, SigningsService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}