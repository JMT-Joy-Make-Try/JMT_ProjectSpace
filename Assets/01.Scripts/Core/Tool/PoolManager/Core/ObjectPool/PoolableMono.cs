using UnityEngine;

namespace JMT.Core.Tool.PoolManager.Core
{
    public abstract class PoolableMono : MonoBehaviour
    {
        public PoolingType type;
        public abstract void ResetItem();
    }
}
