using UnityEngine;

namespace RedPanda.Project.Services.Interfaces
{
    public interface IGameObjectFactory
    {
        T Create<T>(T prefab, Transform parent) where T: MonoBehaviour;
    }
}