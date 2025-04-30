using System.Collections.Generic;
using UnityEngine;

namespace JMT.Core.Tool.PoolManager.Core
{
    public class PoolingManager : MonoSingleton<PoolingManager>
    {
        private Dictionary<PoolingType, Pool> _pools
            = new Dictionary<PoolingType, Pool>();

        public List<PoolingTableSO> listSO;
        private List<IPoolable> _generatedObjects = new List<IPoolable>();


        protected override void Awake()
        {
            base.Awake();
            foreach (PoolingTableSO table in listSO)
            {
                foreach (PoolingItemSO item in table.datas)
                {
                    Debug.Log(item.name);
                    Debug.Log(item.prefab);
                    Debug.Log($"[ Load Pools ] {item.prefab.type.ToString()}");
                    CreatePool(item);
                }
            }
        }

        private void CreatePool(PoolingItemSO item)
        {
            var pool = new Pool(item.prefab, item.prefab.type, transform, item.poolCount);
            _pools.Add(item.prefab.type, pool);
        }

        public IPoolable Pop(PoolingType type)
        {
            if (_pools.ContainsKey(type) == false)
            {
                Debug.LogError($"Prefab dose not exist on Pool : {type}");
                return null;
            }

            IPoolable item = _pools[type].Pop();
            item.ResetItem();
            _generatedObjects.Add(item);
            return item;
        }

        public void Push(IPoolable obj, bool resetParent = false)
        {
            if (resetParent)
                obj.ObjectPrefab.transform.SetParent(transform);
            _pools[obj.type].Push(obj);
            _generatedObjects.Remove(obj);
        }

        public void ResetPool()
        {
            foreach (IPoolable pool in _generatedObjects)
            {
                _pools[pool.type].Push(pool);
            }

            _generatedObjects.Clear();
        }

        public void ResetPool(PoolingType poolType)
        {
            for (int i = _generatedObjects.Count - 1; i >= 0; i--)
            {
                if (_generatedObjects[i].type == poolType)
                {
                    _pools[_generatedObjects[i].type].Push(_generatedObjects[i]);
                }
            }
        }
    }
}
