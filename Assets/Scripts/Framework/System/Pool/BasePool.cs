using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class BasePool
    {
        protected List<GameObject> _pool = new List<GameObject>();

        protected GameObject _prefab;
        protected Transform _root;

        public BasePool(GameObject prefab, Transform root, int initAtStart)
        {
            _root = root;

            if (prefab != null)
                _prefab = prefab;

            // Init pool
            for (int i = 0; i < initAtStart; i++)
            {
                GameObject item = SpawnItem();
                SetActive(item, false);
                _pool.Add(item);
            }
        }

        public T GetItem<T>() where T : Component
        {
            // Find if there is any available item, return it
            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i] != null)
                {
                    if (IsActive(_pool[i]))
                    {
                        continue;
                    }
                    else
                    {
                        GameObject item = _pool[i];

                        // New item active from pool will move to last
                        _pool.RemoveAt(i);
                        _pool.Add(item);

                        SetActive(item, true);
                        return item.GetComponent<T>();
                    }
                }
                else
                {
                    _pool.RemoveAt(i);
                    i = Mathf.Clamp(i, 0, _pool.Count - 1);
                }
            }

            // Check if there is no more item in pool, create new
            _pool.Add(SpawnItem<T>());
            return _pool[_pool.Count-1].GetComponent<T>();
        }

        protected virtual GameObject SpawnItem<T>() where T : Component
        {
            // Create new item and set parent to root
            GameObject newItem = null;

            if (_prefab == null)
            {
                newItem = new GameObject();
                newItem.AddComponent<T>();
                newItem.transform.SetParent(_root);

#if UNITY_EDITOR
                newItem.name = typeof(T).ToString();
#endif
            }
            else
            {
                newItem = GameObject.Instantiate(_prefab, _root);
            }

            return newItem;
        }
        public virtual void ClearItem(GameObject prefab, int keep = 3)
        {
            for (int i = _pool.Count - keep; i < _pool.Count; i++)
            {
                if (_pool[i] != null)
                {
                    _pool[i].gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < _pool.Count - keep; i++)
            {
                GameObject.Destroy(_pool[i]);
            }
            _pool.RemoveRange(0, _pool.Count - keep);
        }
        protected virtual GameObject SpawnItem()
        {
            // Create new item and set parent to root
            GameObject newItem = null;
            newItem = UnityEngine.Object.Instantiate(_prefab, _root);
            return newItem;
        }
        protected virtual bool IsActive(GameObject item)
        {
            return item.activeSelf;
        }

        protected virtual void SetActive(GameObject item, bool enabled)
        {
            item.SetActive(enabled);
        }

    }
}
