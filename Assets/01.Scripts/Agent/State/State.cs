using UnityEngine;

namespace JMT.Agent.State
{
    public abstract class State : MonoBehaviour
    {
        protected AgentAI _agent;
        
        /// <summary>
        /// Init state
        /// </summary>
        /// <param name="agent"></param>
        public virtual void Initialize(AgentAI agent) { }
        
        /// <summary>
        /// EnterState (State 진입)
        /// </summary>
        public virtual void EnterState() { }
        
        /// <summary>
        /// UpdateState (State 업데이트)
        /// </summary>
        public virtual void UpdateState() {}
        
        /// <summary>
        /// ExitState (State 종료)
        /// </summary>
        public virtual void ExitState() {}
    }
}