using JMT.Agent.Trader;
using UnityEngine;

namespace JMT
{
    public class TraderUI : MonoBehaviour, ITraderComponent
    {
        public Trader Trader { get; private set; }

        public void Init(Trader trader)
        {
            Trader = trader;
        }


    }
}
