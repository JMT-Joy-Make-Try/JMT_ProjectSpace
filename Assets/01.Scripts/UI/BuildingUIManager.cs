using JMT.UISystem.Building;
using JMT.UISystem.Factory;
using JMT.UISystem.Hospital;
using JMT.UISystem.Laboratory;
using JMT.UISystem.Station;
using JMT.UISystem.SupplyOxygen;
using UnityEngine;

namespace JMT.UISystem
{
    public class BuildingUIManager : MonoSingleton<BuildingUIManager>
    {
        [SerializeField] private ItemBuildingController itemBuildingCompo;
        [SerializeField] private StationController stationCompo;
        [SerializeField] private StationStorageController storageCompo;
        [SerializeField] private LaboratoryController laboratoryCompo;
        [SerializeField] private SupplyOxygenController oxygenCompo;
        [SerializeField] private HospitalController hospitalCompo;
        [SerializeField] private FactoryController factoryCompo;

        public ItemBuildingController ItemBuildingCompo => itemBuildingCompo;
        public StationController StationCompo => stationCompo;
        public StationStorageController StorageCompo => storageCompo;
        public LaboratoryController LaboratoryCompo => laboratoryCompo;
        public SupplyOxygenController OxygenCompo => oxygenCompo;
        public HospitalController HospitalCompo => hospitalCompo;
        public FactoryController FactoryCompo => factoryCompo;
    }
}
