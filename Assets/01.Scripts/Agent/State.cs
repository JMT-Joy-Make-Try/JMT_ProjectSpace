using UnityEngine;

namespace JMT.Agent.State
{
    public abstract class State<T> : MonoBehaviour where T : System.Enum
    {
        protected AgentAI<T> _agent;
        protected string _stateName;

        /// <summary>
        /// Init state
        /// </summary>
        /// <param name="agent"></param>
        public virtual void Initialize(AgentAI<T> agent, string stateName)
        {
            _agent = agent;
            _stateName = stateName;
        }

        /// <summary>
        /// EnterState (State 진입)
        /// </summary>
        public virtual void EnterState()
        {
            _agent.animator.SetBool(_stateName, true);
        }

        /// <summary>
        /// UpdateState (State 업데이트)
        /// </summary>
        public virtual void UpdateState()
        {
            Debug.Log(_stateName);
        }

        /// <summary>
        /// ExitState (State 종료)
        /// </summary>
        public virtual void ExitState()
        {
            _agent.animator.SetBool(_stateName, false);
        }
    }
}