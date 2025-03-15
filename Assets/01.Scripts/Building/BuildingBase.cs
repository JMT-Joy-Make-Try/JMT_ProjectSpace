using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Core.Tool;
using JMT.Planets.Tile.Items;
using System;
using System.Collections;
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
            StartCoroutine(WorkCoroutine(itemType));
        }

        private IEnumerator WorkCoroutine(ItemType itemType)
        {
            _currentItemType = itemType;
            while (CurrentItems.Count > 0)
            {
                Tuple<ItemType, int> item = CurrentItems.Peek();
                if (item.Item1 == itemType)
                {
                    _progress += 0.1f;
                    if (_progress >= 1)
                    {
                        _progress = 0;
                        RemoveItem(item);
                        Debug.Log("아이템이 제작되었습니다.");
                    }
                }
                yield return new WaitForSeconds(1);
            }
        }

        private void RemoveItem(Tuple<ItemType, int> item)
        {
            if (item.Item2 > 0)
            {
                item = CurrentItems.Dequeue();
                item = new Tuple<ItemType, int>(item.Item1, item.Item2 - 1);
                CurrentItems.Enqueue(item);
            }
            else
            {
                CurrentItems.Dequeue();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetItem(ItemType.Ice, 10);
                foreach (var item in CurrentItems)
                {
                    Debug.Log(item.Item1 + " : " + item.Item2);
                }
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
