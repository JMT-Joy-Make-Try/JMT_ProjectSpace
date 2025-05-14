using AYellowpaper.SerializedCollections;
using JMT.Item;
using JMT.UISystem;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingFixer : MonoBehaviour, IBuildingComponent
    {
        [SerializeField] private SerializedDictionary<ItemSO, int> items;
        [SerializeField] private float _repairTime = 5f;
        
        private float _curRepairAmount;
        
        public BuildingBase Building { get; private set; }
        
        public void Init(BuildingBase building)
        {
            Building = building;
            
            Building.GetBuildingComponent<BuildingHealth>().OnBuildingBroken += HandleBrokenBuilding;
        }

        private void OnDestroy()
        {
            Building.GetBuildingComponent<BuildingHealth>().OnBuildingBroken -= HandleBrokenBuilding;
        }

        private void HandleBrokenBuilding()
        {
            Debug.Log($"Broken Building: {gameObject.name}");
            // 건물 파괴됨
            Building.GetBuildingComponent<BuildingNPC>().RemoveAllNpc();
            Building.StopWork();
            Building.SetLayer("BrokenBuilding");
            
            // 수리 UI 띄우기
        }

        public void RepairBuilding()
        {
            if (!HasItem()) return;
            StartCoroutine(RepairRoutine());
        }

        private bool HasItem()
        {
            foreach (var item in items)
            {
                if (!GameUIManager.Instance.InventoryCompo.HasItem(item.Key, item.Value))
                {
                    Debug.Log("Not enough items to repair");
                    return false;
                }
            }
            return true;
        }

        private IEnumerator RepairRoutine()
        {
            float elapsedTime = 0f;
            _curRepairAmount = 0f;
            while (elapsedTime < _repairTime)
            {
                elapsedTime += Time.deltaTime;
                _curRepairAmount = Mathf.Lerp(0, 1, elapsedTime / _repairTime);
                yield return null;
            }
            
            // 수리 완료
            foreach (var item in items)
            {
                GameUIManager.Instance.InventoryCompo.RemoveItem(item.Key, item.Value);
            }
            
            Building.GetBuildingComponent<BuildingHealth>().InitStat();
        }
    }
}