using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace RogueHelper.Loaders
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Create(Type key, Object[] res);
        void Delete(Type key, T item);
        List<R> GetItem<R>(Type key) where R : class;
    }
}