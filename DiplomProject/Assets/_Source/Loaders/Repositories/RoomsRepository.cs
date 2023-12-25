using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RogueHelper.Loaders.Repositories
{
    public class RoomsRepository : IRepository<GameObject>
    {
        private Dictionary<Type, List<GameObject>> _prefabs = new();

        public void Create(Type key, Object[] res)
        {
            _prefabs.Add(key, new List<GameObject>());
            for (int i = 0; i < res.Length; i++)
            {
                _prefabs[key].Add(res[i] as GameObject);
            }
        }

        public void Delete(Type key, GameObject item)
        {
            if (_prefabs.ContainsKey(key))
                _prefabs[key].Remove(item);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<R> GetItem<R>(Type key) where R : class
        {
            List<R> newList = new();
            foreach (GameObject so in _prefabs[key])
            {
                newList.Add(so as R);
            }
            return newList;
        }
    }
}