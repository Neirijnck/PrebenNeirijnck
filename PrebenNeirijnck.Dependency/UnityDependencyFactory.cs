using Microsoft.Practices.Unity;

namespace PrebenNeirijnck.Dependency
{
    public class UnityDependencyFactory
    {
        public IUnityContainer Create()
        {
            var container = new UnityContainer();
            DependencyResolver.SetResolver(container);
            return container;
        }
    }
}