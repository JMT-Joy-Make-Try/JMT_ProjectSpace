using UnityEngine;
using UnityEngine.Serialization;

namespace JMT.Agent.State
{
    public abstract class State<T> : MonoBehaviour where T : System.Enum
    {
        public AgentAI<T> Agent;
        protected string _stateName;
        protected StateMachine<T> _stateMachine;

        /// <summary>
        /// Init state
        /// </summary>
        /// <param name="agent"></param>
        public virtual void Initialize(AgentAI<T> agent, string stateName)
        {
            Agent = agent;
            _stateName = stateName;
            _stateMachine = Agent.StateMachineCompo;
        }

        /// <summary>
        /// EnterState (State 진입)
        /// </summary>
        public virtual void EnterState()
        {
            Agent.AnimatorCompo.SetBool(_stateName, true);
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
            Agent.AnimatorCompo.SetBool(_stateName, false);
            StopAllCoroutines();
        }

        public virtual void OnAnimationEnd()
        {
            
        }
    }
}