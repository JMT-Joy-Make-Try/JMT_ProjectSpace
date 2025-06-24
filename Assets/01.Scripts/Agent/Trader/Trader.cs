using AYellowpaper.SerializedCollections;
using JMT.Core;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.DataSystem;
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
        public event Action OnInteractEvent;

        private Dictionary<Type, ITraderComponent> _componentLookup = new Dictionary<Type, ITraderComponent>();
        
        
        public override void Init()
        {
            InitTraderComponents();
            base.Init();
            var tradeItem = RandomDataSystem.Instance.GetRandomTraderTradeSO();
            var tradeCompo = GetTraderComponent<TraderTrade>();
            tradeCompo.SetTradeItem(tradeItem);
            StateMachineCompo.ChangeState(TraderStateEnum.Idle);
            GameUIManager.Instance.InteractCompo.OnTraderInteractEvent += Interact;

            tradeCompo.TraderTradeSO.OnStartTradeEvent += BuyStart;
        }

        private void BuyStart()
        {
            StateMachineCompo.ChangeState(TraderStateEnum.Buy);
            Debug.Log($"CurrentState {StateMachineCompo.CurrentState}");
        }


        private void OnDestroy()
        {
            if (GameUIManager.Instance == null) return;
            GameUIManager.Instance.InteractCompo.OnTraderInteractEvent -= Interact;

            var tradeSO = GetTraderComponent<TraderTrade>().TraderTradeSO;
            tradeSO.OnStartTradeEvent -= BuyStart;
        }
        
        public void Interact()
        {
            StateMachineCompo.ChangeState(TraderStateEnum.Interact);
            var tradeSO = GetTraderComponent<TraderTrade>().TraderTradeSO;
            GameUIManager.Instance.TradeCompo.OpenPanel(tradeSO);
            OnInteractEvent?.Invoke();
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