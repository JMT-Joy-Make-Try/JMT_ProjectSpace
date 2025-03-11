using AYellowpaper.SerializedCollections;
using JMT.Planets.Tile.Items;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public abstract class BuildingBase : MonoBehaviour
    {
        [field: SerializeField] public int NpcCount { get; protected set; }
        [Space]
        [Header("Decrease Item")]
        [SerializeField] protected SerializedDictionary<ItemType, int> decreaseItems;
        [Space]
        [Header("Increase Item")]
        [SerializeField] protected SerializedDictionary<ItemType, int> increaseItems;
        
        protected int _currentNpcCount;
        public abstract void Build(Vector3 position);

        protected virtual void Work()
        {
            if (!IsWalkable()) return;
        }

        protected bool IsWalkable()
        {
            return _currentNpcCount >= NpcCount;
        }

        public virtual void AddNpc(int cnt)
        {
            _currentNpcCount += cnt;
        }
        
        public virtual void Upgrade()
        {
        }
    }
}
