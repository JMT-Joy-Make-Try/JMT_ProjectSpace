using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Core.Tool;
using JMT.Planets.Tile;
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
        
        [SerializeField] protected List<NPCAgent> _currentNpc;
        [SerializeField] protected SerializeQueue<SerializeTuple<ItemType, int>> CurrentItems;
        
        protected float _progress;
        protected event Action OnStartWorking;
        protected bool _isWorking;
        
        [Space]
        
        [Header("Debug")]
        [SerializeField] private NPCAgent _npcAgent;
        public abstract void Build(Vector3 position, Transform parent);

        protected void Awake()
        {
            _currentNpc = new List<NPCAgent>();
        }

        protected virtual void Start()
        {
            OnStartWorking += Work;
            CurrentItems = new SerializeQueue<SerializeTuple<ItemType, int>>();
        }

        protected virtual void OnDestroy()
        {
            OnStartWorking -= Work;
        }

        public virtual void Work()
        {
            if (_isWorking)
            {
                return;
            }
            _isWorking = true;
            StartCoroutine(WorkCoroutine());
        }

        protected IEnumerator WorkCoroutine()
        {
            while (CurrentItems.Count > 0)
            {
                SerializeTuple<ItemType, int> item = CurrentItems.Peek();
                Debug.Log($"Working for {item.Item1} : {item.Item2}");
                _progress += 0.1f;
                if (_progress >= 1)
                {
                    _progress = 0;
                    RemoveItem(ref item);
                    Debug.Log("아이템이 제작되었습니다.");
                }
                yield return new WaitForSeconds(1);
            }
            _isWorking = false;
        }

        private void RemoveItem(ref SerializeTuple<ItemType, int> item)
        {
            if (item.Item2 > 0)
            {
                item--;
                if (item.Item2 == 0)
                {
                    CurrentItems.Dequeue();
                }
                InventoryManager.Instance.AddItem(item.Item1, 1);
            }
            else
            {
                CurrentItems.Dequeue();
            }
        }

        #if UNITY_EDITOR
        private void Update()
        {
            // 테스트용 코드
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetItem(ItemType.Ice, 2);
                SetItem(ItemType.Cloth, 2);
                SetItem(ItemType.Seed, 2);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                AddNpc(_npcAgent);
            }
        }
        #endif

        public virtual void AddNpc(NPCAgent agent)
        {
            _currentNpc.Add(agent);
            if (_currentNpc.Count >= NpcCount)
            {
                OnStartWorking?.Invoke();
            }
        }
        
        public virtual void Upgrade()
        {
        }

        protected virtual void SetItem(ItemType type, int amount)
        {
            CurrentItems.Enqueue(new SerializeTuple<ItemType, int>(type, amount));
        }
    }
}
