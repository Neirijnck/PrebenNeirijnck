using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace PrebenNeirijnck.Configuration.Activators
{
    public class UmbracoWebApiHttpControllerActivator : IHttpControllerActivator
    {
        #region Fields
        private readonly DefaultHttpControllerActivator _defaultHttpControllerActivator;
        #endregion

        #region Constructors
        public UmbracoWebApiHttpControllerActivator()
        {
            _defaultHttpControllerActivator = new DefaultHttpControllerActivator();
        }
        #endregion

        #region Functions
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            IHttpController instance = IsUmbracoController(controllerType) ? Activator.CreateInstance(controllerType) as IHttpController : _defaultHttpControllerActivator.Create(request, controllerDescriptor, controllerType);

            return instance;
        }

        private bool IsUmbracoController(Type controllerType)
        {
            return controllerType.Namespace != null && controllerType.Namespace.StartsWith("umbraco", StringComparison.InvariantCultureIgnoreCase);
        }
        #endregion
    }
}