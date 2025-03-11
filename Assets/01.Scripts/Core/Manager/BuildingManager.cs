using JMT.Building;
using UnityEngine;

namespace JMT.Core.Manager
{
    public class BuildingManager : MonoSingleton<BuildingManager>
    {
        [field: SerializeField] public BuildingBase CurrentBuilding { get; private set; }
    }
}