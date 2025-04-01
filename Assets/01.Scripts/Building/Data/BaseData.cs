using JMT.Planets.Tile.Items;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public struct BuildingWork
    {
        public ItemType itemType;
        public TimeData time;
    }
    public class BaseData : MonoBehaviour
    {
        // 알아서 추가하세요
        private int buildingLevel = 1;
        private int useFuelValue = 0;
        private List<BuildingWork> works = new();

        public void AddWork(BuildingWork work) => works.Add(work);
    }
}
