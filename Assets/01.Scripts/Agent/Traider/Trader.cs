using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Agent.Trader
{
    public class Trader : AgentAI<TraderStateEnum>
    {
        private Dictionary<Type, ITraderComponent> _componentLookup = new Dictionary<Type, ITraderComponent>();
        
        public override void Init()
        {
            base.Init();
            InitTraderComponents();
            StateMachineCompo.ChangeState(TraderStateEnum.Idle);
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
            else
            {
                Debug.LogError($"Component of type {typeof(T)} not found in Trader.");
                return default;
            }
        }
    }
}