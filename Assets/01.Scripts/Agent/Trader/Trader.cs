using AYellowpaper.SerializedCollections;
using JMT.Core;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Item;
using JMT.Object;
using JMT.UISystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.Agent.Trader
{
    public class Trader : AgentAI<TraderStateEnum>, IInteractable
    {
        private Dictionary<Type, ITraderComponent> _componentLookup = new Dictionary<Type, ITraderComponent>();
        
        
        public override void Init()
        {
            InitTraderComponents();
            base.Init();
            StateMachineCompo.ChangeState(TraderStateEnum.Idle);
            GameUIManager.Instance.InteractCompo.OnTraderInteractEvent += Interact;
        }

        private void OnDestroy()
        {
            if (GameUIManager.Instance == null) return;
            GameUIManager.Instance.InteractCompo.OnTraderInteractEvent -= Interact;
        }
        
        public void Interact()
        {
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
    }
}