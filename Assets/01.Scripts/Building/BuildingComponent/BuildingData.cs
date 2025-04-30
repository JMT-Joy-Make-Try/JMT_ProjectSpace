using JMT.Core.Tool;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using JMT.UISystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingData : MonoBehaviour, IBuildingComponent
    {
        [Header("Building Data")]
        public List<SerializeTuple<ItemType, int>> CurrentItems;
        private BuildingDataSO buildingData;
        public BuildingBase Building { get; private set; }
        public BuildingDataSO Data => buildingData;
        public void Init(BuildingBase building)
        {
            Building = building;
        }
        
        public void SetItem(ItemType type, int amount)
        {
            if (CurrentItems.Contains(type))
            {
                CurrentItems.Find(x => x.Item1 == type).Item2 += amount;
                return;
            }
            CurrentItems.Add(new SerializeTuple<ItemType, int>(type, amount));
        }

        public void SetBuildingData(BuildingDataSO data)
        {
            buildingData = data;
        }
        
        public void AddPlayerInventory()
        {
            foreach (var item in CurrentItems)
            {
                GameUIManager.Instance.InventoryCompo.AddItem(item.Item1, item.Item2);
            }
        }
        
        public void SetBuildingData(BuildingDataSO data, PVCBuilding pvc)
        {
            SetBuildingData(data);
            Building.SetPVCBuilding(pvc);
            Building.PVC.SetBuildTime(data.buildingLevel[0].BuildTime);
            Building.Building();
        }
    }
}