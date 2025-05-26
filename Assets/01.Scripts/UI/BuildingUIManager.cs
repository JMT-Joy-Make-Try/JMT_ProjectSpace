using JMT.UISystem.Building;
using JMT.UISystem.Laboratory;
using JMT.UISystem.SupplyOxygen;
using UnityEngine;

namespace JMT.UISystem
{
    public class BuildingUIManager : MonoSingleton<BuildingUIManager>
    {
        [SerializeField] private ItemBuildingController itemBuildingCompo;
        [SerializeField] private StationController stationCompo;
        [SerializeField] private LaboratoryController laboratoryCompo;
        [SerializeField] private SupplyOxygenController oxygenCompo;

        public ItemBuildingController ItemBuildingCompo => itemBuildingCompo;
        public StationController StationCompo => stationCompo;
        public LaboratoryController LaboratoryCompo => laboratoryCompo;
        public SupplyOxygenController OxygenCompo => oxygenCompo;
    }
}
