using JMT.Building;
using JMT.Core.Manager;
using UnityEngine;
namespace JMT.Item
{
    [CreateAssetMenu(menuName = "SO/Data/Items/StructItemSO", fileName = "StructItemSO")]
    public class StructItemSO : ItemSO
    {
        public BuildingDataSO BuildingDataSO;

        public void OnEquip()
        {
            BuildingManager.Instance.AddBuildingDataSO(BuildingDataSO);
        }
    }
}