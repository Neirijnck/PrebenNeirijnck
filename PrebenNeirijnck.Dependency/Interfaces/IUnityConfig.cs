using Microsoft.Practices.Unity;

namespace PrebenNeirijnck.Dependency.Interfaces
{
    public interface IUnityConfig
    {
        void RegisterComponents(IUnityContainer container);
    }
}