using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace JMT.Agent
{
    public class StateMachine : MonoBehaviour
    {
        // State들을 모아놓은 Dictionary
        [SerializeField] private SerializedDictionary<StateEnum, State.State> states;
        
        // 현재 State
        private State.State _currentState;

        /// <summary>
        /// State 변경
        /// </summary>
        /// <param name="state">바꿀 State</param>
        public void ChangeState(StateEnum state)
        {
            if (_currentState != null)
            {
                _currentState.ExitState();
            }
            _currentState = states[state];
            _currentState.EnterState();
        }

        /// <summary>
        /// State 초기화
        /// </summary>
        /// <param name="agent">State Machine을 돌릴 Agent</param>
        public void InitState(AgentAI agent)
        {
            foreach (var state in states)
            {
                state.Value.Initialize(agent);
            }
        }
        
        /// <summary>
        /// State 업데이트
        /// </summary>
        public void UpdateState()
        {
            _currentState.UpdateState();
        }
    }
}