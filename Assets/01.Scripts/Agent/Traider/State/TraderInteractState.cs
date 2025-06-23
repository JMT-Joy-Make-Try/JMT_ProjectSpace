using JMT.Agent.State;

namespace JMT.Agent.Trader
{
    public class TraderInteractState : State<TraderStateEnum>
    {
        private Trader _trader;

        public override void Initialize(AgentAI<TraderStateEnum> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            _trader = agent as Trader;
        }

        public override void EnterState()
        {
            base.EnterState();
            _trader.GetTraderComponent<TraderTrade>().SetTradeItem(_trader.TradeItems);
        }
    }
}