using JMT.UISystem.Building;
using UnityEngine;

namespace JMT.UISystem
{
    public class BuildingUIManager : MonoSingleton<BuildingUIManager>
    {
        [SerializeField] private ItemBuildingController itemBuildingCompo;
        [SerializeField] private StationController stationCompo;

        public ItemBuildingController ItemBuildingCompo => itemBuildingCompo;
        public StationController StationCompo => stationCompo;
    }
}
