using Grace.DependencyInjection;
using RedPanda.Project.Services.Interfaces;
using UnityEngine;

namespace RedPanda.Project.Services
{
    public class GameObjectFactory : IGameObjectFactory
    {
        private readonly IExportLocatorScope _container;

        public GameObjectFactory(IExportLocatorScope container)
        {
            _container = container;
        }
        
        public T Create<T>(T prefab, Transform parent) where T: MonoBehaviour
        {
            T component = Object.Instantiate(prefab, parent);
            _container.Inject(component);
            return component;
        }
    }
}