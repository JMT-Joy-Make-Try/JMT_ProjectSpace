using JMT.Core.Tool.PoolManager.Core;
using UnityEngine;

namespace JMT.Object
{
    public class BlackHole : MonoBehaviour, ISpawnable, IPoolable
    {
        public void Spawn(Vector3 position)
        {
            
        }

        [field: SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;
        public void ResetItem()
        {
            
        }
    }
}
