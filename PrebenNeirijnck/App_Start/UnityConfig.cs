using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using NLog.Fluent;
using PrebenNeirijnck.Configuration.Activators;
using PrebenNeirijnck.Dependency;
using PrebenNeirijnck.Dependency.Interfaces;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace PrebenNeirijnck.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var dependencyFactory = new UnityDependencyFactory();
            IUnityContainer container = dependencyFactory.Create();

            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();

            var types = assemblies.Where(x => x.FullName.Contains("PrebenNeirijnck.")).SelectMany(x => x.GetTypes()).Where(type => typeof(IUnityConfig).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract);

            foreach (var type in types)
            {
                try
                {
                    var config = (IUnityConfig)Activator.CreateInstance(type);
                    config.RegisterComponents(container);
                }
                catch (Exception ex)
                {
                    Log.Error().Message(ex.Message).Exception(ex).Write();
                }
            }

            // Workaround to keep Umbraco office API controllers working
            container.RegisterType<IHttpControllerActivator, UmbracoWebApiHttpControllerActivator>();

            System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}