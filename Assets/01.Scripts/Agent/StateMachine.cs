using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using System.Collections;
using UnityEngine;

namespace JMT.Agent
{
    public class StateMachine<T> : MonoBehaviour where T : Enum
    {
        // State들을 모아놓은 Dictionary
        [SerializeField] private SerializedDictionary<T, State.State<T>> states;

        public SerializedDictionary<T, State.State<T>> States
        {
            get => states; 
            set => states = value;
        }
        
        // 현재 State
        private State.State<T> _currentState;
        public State.State<T> CurrentState => _currentState;

        /// <summary>
        /// State 변경
        /// </summary>
        /// <param name="state">바꿀 State</param>
        public void ChangeState(T state)
        {
            if (_currentState != null)
            {
                _currentState.ExitState();
            }
            _currentState = states[state];
            _currentState.EnterState();
        }

        public void ChangeStateDelay(T state, float delayTime)
        {
            if (_currentState != null)
            {
                _currentState.ExitState();
            }
            _currentState = states[state];
            StartCoroutine(Change(delayTime));
        }

        private IEnumerator Change(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            _currentState.ExitState();
        }

        /// <summary>
        /// State 초기화
        /// </summary>
        /// <param name="agent">State Machine을 돌릴 Agent</param>
        public void InitAllState(AgentAI<T> agent)
        {
            foreach (var state in states)
            {
                state.Value.Initialize(agent, state.Key.ToString());
            }
        }
        
        public void InitState(AgentAI<T> agent, T state)
        {
            states[state].Initialize(agent, state.ToString());
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