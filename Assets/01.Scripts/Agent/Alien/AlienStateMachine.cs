using System;

namespace JMT.Agent.Alien
{
    public class AlienStateMachine : StateMachine<AlienState>
    {
        private Alien _alien;
        private void Awake()
        {
            OnStateChange += OnStateChangeHandler;
        }

        private void OnDestroy()
        {
            OnStateChange -= OnStateChangeHandler;
        }

        private void Start()
        {
            _alien = CurrentState.Agent as Alien;
            OnStateChangeHandler(AlienState.Idle);
        }

        private void OnStateChangeHandler(AlienState state)
        {
            if (_alien == null) return;
            _alien.ChangeColor(state);
        }
    }
}