using UnityEngine;

namespace JMT.Building
{
    public abstract class BuildingBase : MonoBehaviour
    {
        protected abstract void Build(Vector3 position);
        protected abstract void Work(int npcCount);
        protected abstract bool CanBuild();
    }
}
