using AYellowpaper.SerializedCollections;
using JMT.Core;
using JMT.Item;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.Trader
{
    public class Trader : AgentAI<TraderStateEnum>, IInteractable, IItemReceivable
    {
        [SerializeField] private SerializedDictionary<ItemSO, int> _tradeItems = new SerializedDictionary<ItemSO, int>();
        private Dictionary<Type, ITraderComponent> _componentLookup = new Dictionary<Type, ITraderComponent>();
        
        public SerializedDictionary<ItemSO, int> TradeItems => _tradeItems;
        public event Action OnInteract;
        
        public override void Init()
        {
            base.Init();
            InitTraderComponents();
            StateMachineCompo.ChangeState(TraderStateEnum.Idle);
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
                return true;
            }

            Debug.LogWarning($"Item {item.name} not found in trade items.");
            return false;
        }
    }
}