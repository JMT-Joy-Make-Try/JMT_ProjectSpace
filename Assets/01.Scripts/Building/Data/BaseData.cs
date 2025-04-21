using JMT.Core.Tool;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System;
using System.Collections.Generic;
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
        [SerializeField] private SizeLimitQueue<BuildingWork> works = new(4);

        public SizeLimitQueue<BuildingWork> Works => works;

        public virtual void AddWork(BuildingWork work)
        {
            if (works.IsFull()) return;
            works.Enqueue(work);
        }

        public virtual void RemoveWork()
        {
            var work = works.Dequeue();
        }
    }
}
