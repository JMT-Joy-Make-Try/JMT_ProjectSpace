using JMT.Building;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem
{
    public class WorkUI : PanelUI
    {
        [SerializeField] private Transform content;
        [SerializeField] private BuildingCellUI buildingPrefab;
        public void SetBuilding(List<BuildingDataSO> data)
        {
            ResetContent();
            for(int i = 0; i < data.Count; i++)
            {
                BuildingCellUI cell = Instantiate(buildingPrefab, content);
                cell.Init(data[i]);
            }
        }

        private void ResetContent()
        {
            for(int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i));
            }
        }
    }
}
