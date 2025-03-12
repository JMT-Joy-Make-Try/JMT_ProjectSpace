using AYellowpaper.SerializedCollections;
using JMT.Planets.Tile.Items;
using System;
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
        
        protected event Action OnStartWorking;
        public abstract void Build(Vector3 position);

        protected virtual void Start()
        {
            OnStartWorking += Work;
        }

        protected virtual void OnDestroy()
        {
            OnStartWorking -= Work;
        }

        public abstract void Work();

        public virtual void AddNpc(int cnt)
        {
            _currentNpcCount += cnt;
            if (_currentNpcCount > NpcCount)
            {
                _currentNpcCount = NpcCount;
                OnStartWorking?.Invoke();
            }
        }
        
        public virtual void Upgrade()
        {
        }
    }
}
