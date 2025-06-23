using AYellowpaper.SerializedCollections;
using JMT.Core;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Item;
using JMT.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Agent.Trader
{
    public class Trader : AgentAI<TraderStateEnum>, IInteractable, IItemReceivable
    {
        [SerializeField] private SerializedDictionary<ItemSO, int> _tradeItems = new SerializedDictionary<ItemSO, int>();
        [SerializeField] private SerializedDictionary<ItemSO, int> _dropItems = new SerializedDictionary<ItemSO, int>();
        private Dictionary<Type, ITraderComponent> _componentLookup = new Dictionary<Type, ITraderComponent>();
        
        public SerializedDictionary<ItemSO, int> TradeItems => _tradeItems;
        public event Action OnInteract;
        
        public override void Init()
        {
            InitTraderComponents();
            base.Init();
            StateMachineCompo.ChangeState(TraderStateEnum.Idle);
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReceiveItem(_tradeItems.Keys.First(), 1);
            }
        }

        public void Interact()
        {
            OnInteract?.Invoke();
            StateMachineCompo.ChangeState(TraderStateEnum.Interact);
        }

        private void InitTraderComponents()
        {
            var components = GetComponents<ITraderComponent>();
            foreach (var component in components)
            {
                if (!_componentLookup.ContainsKey(component.GetType()))
                {
                    _componentLookup.Add(component.GetType(), component);
                    component?.Init(this);
                }
                else
                {
                    Debug.LogWarning($"Component of type {component.GetType()} is already registered in Trader.");
                }
            }
        }

        public T GetTraderComponent<T>() where T : ITraderComponent
        {
            if (_componentLookup.TryGetValue(typeof(T), out var component))
            {
                return (T)component;
            }

            Debug.LogError($"Component of type {typeof(T)} not found in Trader.");
            return default;
        }

        public bool ReceiveItem(ItemSO item, int amount)
        {
            
            if (_tradeItems.ContainsKey(item))
            {
                _tradeItems[item] -= amount;
                if (_tradeItems[item] <= 0)
                {
                    _tradeItems.Remove(item);
                }
                if (_tradeItems.Count <= 0)
                {
                    foreach (var items in _dropItems)
                    {
                        for (int i = 0; i < items.Value; i++)
                        {
                            var itemObj = PoolingManager.Instance.Pop(PoolingType.Item) as ItemObject;
                            itemObj.SetItemType(items.Key);
                            itemObj.transform.position = transform.position + Vector3.up * 0.5f;
                            itemObj.transform.rotation = Quaternion.identity;
                            itemObj.IsCollectable = true;
                        }
                    }
                }
                return true;
            }

            Debug.LogWarning($"Item {item.name} not found in trade items.");
            return false;
        }
    }
}