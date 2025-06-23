namespace JMT.Agent.Trader
{
    public interface ITraderComponent
    {
        Trader Trader { get; }
        void Init(Trader trader);
    }
}