using JMT.Agent.State;
using System;

namespace JMT.Agent.Trader
{
    public class TraderBuyState : State<TraderStateEnum>
    {
        private Trader _trader;
        private TraderTimer _traderTimer;

        public override void Initialize(AgentAI<TraderStateEnum> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _trader = agent as Trader;
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
            _traderTimer.StartTimer();
        }

        private void HandleTimerComplete()
        {
            _trader.StateMachineCompo.ChangeState(TraderStateEnum.Disappear);
        } 
    }
}