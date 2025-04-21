using JMT.Core.Tool;
using JMT.Planets.Tile.Items;
using JMT.DayTime;
using System;
using UnityEngine;

namespace JMT.Building
{
    [Serializable]
    public struct BuildingWork
    {
        public ItemType type;
        public TimeData time;

        public BuildingWork(ItemType type, TimeData time)
        {
            this.type = type;
            this.time = time;
        }
    }
    
    [Serializable]
    public class BaseData
    {
        // 알아서 추가하세요
        public int BuildingLevel = 1;
        public int UseFuelValue = 0;
        
        protected BuildingBase _buildingBase;

        public void Init(BuildingBase buildingBase)
        {
            _buildingBase = buildingBase;
        }
        [SerializeField] private SerializeQueue<BuildingWork> works = new();

        public SerializeQueue<BuildingWork> Works => works;

        public virtual void AddWork(BuildingWork work)
        {
            works.Enqueue(work);
        }

        public virtual void RemoveWork()
        {
            var work = works.Dequeue();
        }
    }
}
