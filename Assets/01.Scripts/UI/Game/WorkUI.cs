using JMT.Building;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem
{
    public class WorkUI : PanelUI
    {
        [SerializeField] private Transform content;
        [SerializeField] private BuildingCellUI buildingPrefab;
        [SerializeField] private int buildingCellCount = 0;
        private readonly List<BuildingCellUI> buildingCells = new();

        private void Awake()
        {
            for (int i = 0; i < buildingCellCount; i++)
            {
                var cell = Instantiate(buildingPrefab, content);
                cell.gameObject.SetActive(false);
                buildingCells.Add(cell);
            }
        }

        public void SetBuilding(List<BuildingDataSO> data)
        {
            int dataCount = data.Count;
            int cellCount = buildingCells.Count;

            for (int i = 0; i < dataCount; i++)
            {
                if (i >= cellCount)
                {
                    var cell = Instantiate(buildingPrefab, content);
                    cell.gameObject.SetActive(false);
                    buildingCells.Add(cell);
                }

                buildingCells[i].gameObject.SetActive(true);
                buildingCells[i].Init(data[i]);
            }

            for (int i = dataCount; i < cellCount; i++)
            {
                buildingCells[i].gameObject.SetActive(false);
            }
        }
    }
}