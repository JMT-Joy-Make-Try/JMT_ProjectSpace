using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Core.Tool;
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
        
        protected List<NPCAgent> _currentNpc;
        protected ItemType _currentItemType;
        
        protected Queue<Tuple<ItemType, int>> CurrentItems;
        
        protected float _progress;
        protected event Action<ItemType> OnStartWorking;
        public abstract void Build(Vector3 position, Transform parent);

        protected virtual void Start()
        {
            OnStartWorking += Work;
            CurrentItems = new Queue<Tuple<ItemType, int>>();
        }

        protected virtual void OnDestroy()
        {
            OnStartWorking -= Work;
        }

        public virtual void Work(ItemType itemType)
        {
            _currentItemType = itemType;
            if (!CurrentItems.Contains(new Tuple<ItemType, int>(itemType, 0)))
            {
                Debug.Log("아이템이 없습니다.");
            }
            else
            {
                Debug .Log("아이템이 있습니다.");
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetItem(ItemType.Ice, 10);
                Work(ItemType.Cloth);
            }
        }

        public virtual void AddNpc(NPCAgent agent)
        {
            _currentNpc.Add(agent);
            if (_currentNpc.Count > NpcCount)
            {
                OnStartWorking?.Invoke(_currentItemType);
            }
        }
        
        public virtual void Upgrade()
        {
        }

        protected virtual void SetItem(ItemType type, int amount)
        {
            CurrentItems.Enqueue(new Tuple<ItemType, int>(type, amount));
        }
    }
}
