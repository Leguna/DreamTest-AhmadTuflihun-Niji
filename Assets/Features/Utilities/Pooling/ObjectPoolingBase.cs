using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.Pooling
{
    public class ObjectPoolingBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected T prefab;
        private bool _parentToPooler = true;
        private Transform _poolParent;

        private List<T> _pool;

        public void Init(T newPrefab, int newPoolSize = 10, bool parentToPooler = true)
        {
            prefab = newPrefab;
            _parentToPooler = parentToPooler;
            _poolParent = _parentToPooler ? transform : null;
            _pool = new List<T>(newPoolSize);
            for (var i = 0; i < newPoolSize; i++)
            {
                var newItem = Instantiate(prefab, _poolParent);
                newItem.gameObject.SetActive(false);
                _pool.Add(newItem);
            }
        }

        public T GetObjectFromPool()
        {
            foreach (var item in _pool.Where(item => !item.gameObject.activeInHierarchy))
            {
                item.gameObject.SetActive(true);
                return item;
            }

            var newItem = Instantiate(prefab, _poolParent);
            _pool.Add(newItem);
            newItem.gameObject.SetActive(true);

            return newItem;
        }

        public T ReturnObjectToPool(T item)
        {
            item.gameObject.SetActive(false);
            return item;
        }

        protected int GetActiveObjectCount()
        {
            return _pool.Count(item => item.gameObject.activeInHierarchy);
        }
    }
}