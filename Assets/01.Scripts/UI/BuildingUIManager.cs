using JMT.UISystem.Building;
using JMT.UISystem.Laboratory;
using UnityEngine;

namespace JMT.UISystem
{
    public class BuildingUIManager : MonoSingleton<BuildingUIManager>
    {
        [SerializeField] private ItemBuildingController itemBuildingCompo;
        [SerializeField] private StationController stationCompo;
        [SerializeField] private LaboratoryController laboratoryCompo;

        public ItemBuildingController ItemBuildingCompo => itemBuildingCompo;
        public StationController StationCompo => stationCompo;
        public LaboratoryController LaboratoryCompo => laboratoryCompo;
    }
}
