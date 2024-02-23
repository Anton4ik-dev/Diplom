using System.Collections.Generic;
using UnityEngine;

namespace RogueHelper.Characters
{
    public class BulletPool<T> where T : MonoBehaviour
    {
        private T _prefab;
        private bool _autoExpand;
        private Transform _container;
        private List<T> _pool;

        public BulletPool(T prefab, bool autoExpand, Transform container, int count)
        {
            _prefab = prefab;
            _autoExpand = autoExpand;
            _container = container;

            CreatePool(count);
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out T element))
                return element;

            if (_autoExpand)
                return CreateObject(true);

            throw new System.Exception("No free elements");
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for (int i = 0; i < count; i++)
                CreateObject();
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            T createdObject = GameObject.Instantiate(_prefab, _container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }

        private bool HasFreeElement(out T element)
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].gameObject.activeInHierarchy)
                {
                    element = _pool[i];
                    _pool[i].transform.position = _container.position;
                    _pool[i].transform.rotation = _container.rotation;
                    _pool[i].gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }
    }
}