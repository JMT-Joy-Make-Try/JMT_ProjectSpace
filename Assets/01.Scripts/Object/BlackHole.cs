using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using UnityEngine;

namespace JMT.Object
{
    public class BlackHole : MonoBehaviour, ISpawnable, IPoolable
    {
        public GameObject Spawn(Vector3 position)
        {
            return gameObject;
        }

        [field: SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;
        public void ResetItem()
        {
            
        }
    }
}
