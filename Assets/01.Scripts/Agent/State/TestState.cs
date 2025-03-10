using UnityEngine;

namespace JMT.Agent.State
{
    public class TestState : State
    {
        public override void Initialize(AgentAI agent)
        {
            Debug.Log(agent.name);
        }
        
        public override void EnterState()
        {
            Debug.Log("Enter TestState");
        }
        
        public override void UpdateState()
        {
            Debug.Log("Update TestState");
        }
        
        public override void ExitState()
        {
            Debug.Log("Exit TestState");
        }
    }
}