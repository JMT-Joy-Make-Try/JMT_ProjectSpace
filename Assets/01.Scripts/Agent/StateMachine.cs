using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using System.Collections;
using UnityEngine;

namespace JMT.Agent
{
    public class StateMachine<T> : MonoBehaviour where T : Enum
    {
        [SerializeField] private AgentAI<T> _agent;
        // State들을 모아놓은 Dictionary
        [SerializeField] private SerializedDictionary<T, State.State<T>> states;
        protected event Action<T> OnStateChange;
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
        /// <param name="force">강제로 바꿀지 여부</param>
        public void ChangeState(T state, bool force = false)
        {
            if (_agent.IsDead && force == false) return;
            if (_currentState != null)
            {
                _currentState.Agent.AnimationEndTrigger.OnAnimationEnd -= _currentState.OnAnimationEnd;
                _currentState.ExitState();
            }
            _currentState = states[state];
            _currentState.EnterState();
            _currentState.Agent.AnimationEndTrigger.OnAnimationEnd += _currentState.OnAnimationEnd;
            OnStateChange?.Invoke(state);
        }

        public void ChangeStateDelay(T state, float delayTime)
        {
            if (_agent.IsDead) return;
            StartCoroutine(Change(delayTime, state));
        }

        private IEnumerator Change(float delayTime, T state)
        {
            yield return new WaitForSeconds(delayTime);
            if (_currentState != null)
            {
                _currentState.ExitState();
            }
            _currentState = states[state];
            _currentState.ExitState();
            OnStateChange?.Invoke(state);
        }
        
        public void ChangeStateWait(T state, bool waitUntil)
        {
            if (_agent.IsDead) return;
            StartCoroutine(Wait(waitUntil, state));
        }

        private IEnumerator Wait(bool waitUntil, T state)
        {
            yield return new WaitUntil(() => waitUntil);
            if (_currentState != null)
            {
                _currentState.ExitState();
            }
            _currentState = states[state];
            _currentState.ExitState();
            OnStateChange?.Invoke(state);
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
            if (_currentState == null) Debug.LogError("Current State is null");
                _currentState.UpdateState();
        }
    }
}