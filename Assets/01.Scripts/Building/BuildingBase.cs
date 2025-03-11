using UnityEngine;

namespace JMT.Building
{
    public abstract class BuildingBase : MonoBehaviour
    {
        [field: SerializeField] public int NpcCount { get; protected set; }
        protected int _currentNpcCount;
        public abstract void Build(Vector3 position);
        protected abstract void Work();

        public virtual void AddNpc(int cnt)
        {
            _currentNpcCount += cnt;
        }
    }
}
