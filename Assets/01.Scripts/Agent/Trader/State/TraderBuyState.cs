using JMT.Agent.State;
using JMT.Agent.Trader;
using System;

namespace JMT.Agent.State
{
    public class TraderBuyState : State<TraderStateEnum>
    {
        private Trader.Trader _trader;
        private TraderTimer _traderTimer;

        public override void Initialize(AgentAI<TraderStateEnum> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _trader = agent as Trader.Trader;
            _traderTimer = _trader.GetTraderComponent<TraderTimer>();
            
            _traderTimer.OnTimerComplete += HandleTimerComplete;
        }

        private void OnDestroy()
        {
            if (_traderTimer != null)
            {
                _traderTimer.OnTimerComplete -= HandleTimerComplete;
            }
        }
        
        public override void EnterState()
        {
            base.EnterState();
            _traderTimer.SetTimer(20);
            _traderTimer.StartTimer();
        }

        private void HandleTimerComplete()
        {
            _trader.StateMachineCompo.ChangeState(TraderStateEnum.Disappear);
        } 
    }
}