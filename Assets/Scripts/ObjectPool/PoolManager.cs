using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using CustomHelpers;
using UnityEngine;

namespace ObjectPool
{
    public class PoolManager : Singleton<PoolManager>
    {
        [SerializedDictionary("Prefab", "Pool Size")]
        [SerializeField]
        private SerializedDictionary<GameObject, int> poolDictionary;

        private Dictionary<GameObject, Pool> pools = new Dictionary<GameObject, Pool>();
        
        protected override void Awake()
        {
            base.Awake();
            foreach (var _pair in poolDictionary)
            {
                var _poolable = _pair.Key.GetOrAddComponent<Poolable>();
                pools.Add(_pair.Key, new Pool(_poolable, transform, _pair.Value));
            }
        }

        private void OnDestroy()
        {
            foreach (var _pair in pools)
            {
                _pair.Value.DestroyPool();
            }
            pools.Clear();
        }

        public void CreatePool(GameObject prefab_, int count_ = 1)
        {
            if(pools.ContainsKey(prefab_)) return;
            
            var _poolable = prefab_.GetOrAddComponent<Poolable>();
            pools.Add(prefab_, new Pool(_poolable, transform, count_));
        }

        public void DestroyPool(GameObject prefab_, bool willDestroyActive_ = false)
        {
            if(!pools.TryGetValue(prefab_, out Pool _pool)) return;
            
            _pool.DestroyPool(willDestroyActive_);
            pools.Remove(prefab_);
        }

        #region Getters and returns
        
        public GameObject Get(GameObject prefab_)
        {
            CreatePool(prefab_);
            return pools[prefab_].Get();
        }
        
        public GameObject Get(GameObject prefab_, Transform parent_)
        {
            CreatePool(prefab_);
            return pools[prefab_].Get(parent_);
        }
        
        public GameObject Get(GameObject prefab_, Vector3 position_, Quaternion rotation_)
        {
            CreatePool(prefab_);
            return pools[prefab_].Get(position_, rotation_);
        }
        
        public GameObject Get(GameObject prefab_, Vector3 position_, Quaternion rotation_, Transform parent_)
        {
            CreatePool(prefab_);
            return pools[prefab_].Get(position_, rotation_, parent_);
        }
        
        public T Get <T>(GameObject prefab_) where T : Component
        {
            CreatePool(prefab_);
            return pools[prefab_].Get<T>();
        }
        
        public T Get <T>(GameObject prefab_, Transform parent_) where T : Component
        {
            CreatePool(prefab_);
            return pools[prefab_].Get<T>(parent_);
        }
        
        public T Get<T>(GameObject prefab_, Vector3 position_, Quaternion rotation_) where T : Component
        {
            CreatePool(prefab_);
            return pools[prefab_].Get<T>(position_, rotation_);
        }
        
        public T Get<T>(GameObject prefab_, Vector3 position_, Quaternion rotation_, Transform parent_) where T : Component
        {
            CreatePool(prefab_);
            return pools[prefab_].Get<T>(position_, rotation_, parent_);
        }
        
        public void Return(GameObject obj_)
        {
            var _poolable = obj_.GetComponent<Poolable>();
            if (_poolable == null)
            {
                Debug.LogError($"Object {obj_.name} is not poolable");
                return;
            }
            _poolable.Release();
        }
        
        #endregion

        #region Static Methods
        
        public static void CreatePoolInstance(GameObject prefab_, int count_ = 1)
        {
            if(Instance.pools.ContainsKey(prefab_)) return;
            
            var _poolable = prefab_.GetOrAddComponent<Poolable>();
            Instance.pools.Add(prefab_, new Pool(_poolable, Instance.transform, count_));
        }
        
        public static GameObject GetInstance(GameObject prefab_)
        {
            Instance.CreatePool(prefab_);
            return Instance.pools[prefab_].Get();
        }
        
        public static GameObject GetInstance(GameObject prefab_, Transform parent_)
        {
            Instance.CreatePool(prefab_);
            return Instance.pools[prefab_].Get(parent_);
        }
        
        public static GameObject GetInstance(GameObject prefab_, Vector3 position_, Quaternion rotation_)
        {
            Instance.CreatePool(prefab_);
            return Instance.pools[prefab_].Get(position_, rotation_);
        }
        
        public static GameObject GetInstance(GameObject prefab_, Vector3 position_, Quaternion rotation_, Transform parent_)
        {
            Instance.CreatePool(prefab_);
            return Instance.pools[prefab_].Get(position_, rotation_, parent_);
        }
        
        public static T GetInstance <T>(GameObject prefab_) where T : Component
        {
            Instance.CreatePool(prefab_);
            return Instance.pools[prefab_].Get<T>();
        }
        
        public static T GetInstance <T>(GameObject prefab_, Transform parent_) where T : Component
        {
            Instance.CreatePool(prefab_);
            return Instance.pools[prefab_].Get<T>(parent_);
        }
        
        public static T GetInstance<T>(GameObject prefab_, Vector3 position_, Quaternion rotation_) where T : Component
        {
            Instance.CreatePool(prefab_);
            return Instance.pools[prefab_].Get<T>(position_, rotation_);
        }
        
        public static T GetInstance<T>(GameObject prefab_, Vector3 position_, Quaternion rotation_, Transform parent_) where T : Component
        {
            Instance.CreatePool(prefab_);
            return Instance.pools[prefab_].Get<T>(position_, rotation_, parent_);
        }

        public static void ReturnInstance(GameObject obj_)
        {
            Instance.Return(obj_);
        }
        
        #endregion
    }
}