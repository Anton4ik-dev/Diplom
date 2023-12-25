using System;
using Object = UnityEngine.Object;

namespace RogueHelper.Loaders
{
    public interface IResourceLoader
    {
        void LoadResource<T>(string path, Type key, IRepository<T> repository) where T : class;
        void UnloadResource<T>(Object asset, Type key, IRepository<T> repository) where T : class;
    }
}