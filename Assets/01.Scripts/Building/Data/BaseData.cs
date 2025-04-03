using JMT.Planets.Tile.Items;
using System;
using System.Collections.Generic;

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
    
    public class BaseData
    {
        // 알아서 추가하세요
        public int BuildingLevel = 1;
        public int UseFuelValue = 0;
        private Queue<BuildingWork> works = new();

        public Queue<BuildingWork> Works => works;
        public void AddWork(BuildingWork work) => works.Enqueue(work);
        public void RemoveWork() => works.Dequeue();
    }
}
