using JMT.Agent.State;
using JMT.Agent.Trader;

namespace JMT.Agent.State
{
    public class TraderInteractState : State<TraderStateEnum>
    {
        private Trader.Trader _trader;

        public override void Initialize(AgentAI<TraderStateEnum> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _trader = agent as Trader.Trader;
        }

        public override void EnterState()
        {
            base.EnterState();
            // 다이얼로그 띄워주기
            BuyStart();
        }

        private void BuyStart()
        {
            _trader.GetTraderComponent<TraderTrade>().SetTradeItem(_trader.TradeItems);
            _stateMachine.ChangeState(TraderStateEnum.Buy);
        }
    }
}