using UnityEngine;
using UnityEngine.Serialization;

namespace JMT.Agent.State
{
    public abstract class State<T> : MonoBehaviour where T : System.Enum
    {
        public AgentAI<T> Agent;
        public string StateName;
        protected StateMachine<T> _stateMachine;

        /// <summary>
        /// Init state
        /// </summary>
        /// <param name="agent"></param>
        public virtual void Initialize(AgentAI<T> agent, string stateName)
        {
            Agent = agent;
            StateName = stateName;
            _stateMachine = Agent.StateMachineCompo;
        }

        /// <summary>
        /// EnterState (State 진입)
        /// </summary>
        public virtual void EnterState()
        {
            Agent.AnimatorCompo.SetBool(StateName, true);
        }

        /// <summary>
        /// UpdateState (State 업데이트)
        /// </summary>
        public virtual void UpdateState()
        {
        }

        /// <summary>
        /// ExitState (State 종료)
        /// </summary>
        public virtual void ExitState()
        {
            Agent.AnimatorCompo.SetBool(StateName, false);
            StopAllCoroutines();
        }

        public virtual void OnAnimationEnd()
        {
            
        }
    }
}