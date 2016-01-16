using Microsoft.Practices.Unity;
using System;

namespace PrebenNeirijnck.Dependency
{
    public static class DependencyResolver
    {
        #region Fields
        private static UnityContainer _container;
        #endregion

        #region Functions
        public static T GetInstance<T>()
        {
            if (_container == null) { throw new NullReferenceException("Unitycontainer can't be null"); }

            return _container.Resolve<T>();
        }

        internal static void SetResolver(UnityContainer container)
        {
            if (_container != null) { throw new ArgumentException("Unitycontainer has already been set"); }

            _container = container;
        }
        #endregion
    }
}