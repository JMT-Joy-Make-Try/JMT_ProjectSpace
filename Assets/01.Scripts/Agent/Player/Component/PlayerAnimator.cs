using JMT.Agent;
using JMT.UISystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerAnimator : MonoBehaviour, IPlayerComponent
    {
        public Player Player { get; private set; }
        public Animator AnimCompo { get; private set; }
        public AnimationEndTrigger EndTrigger { get; private set; }

        private Dictionary<PlayerState, int> stateHash;

        [SerializeField] private PlayerState curState;
        private PlayerState saveState;
        private float coolTime = 0.4f;

        private void Awake()
        {
            stateHash = new Dictionary<PlayerState, int>();
        }
        
        public void Init(Player player)
        {
            Player = player;
            AnimCompo = Player.VisualTrm.GetComponent<Animator>();
            EndTrigger = Player.VisualTrm.GetComponent<AnimationEndTrigger>();

            InitState();
        }

        private void Start()
        {
            GameUIManager.Instance.InteractCompo.OnHoldEvent += HandleHoldEvent;
            Player.InputSO.OnMoveEvent += HandleMoveAnimation;
        }

        private void OnDestroy()
        {
            GameUIManager.Instance.InteractCompo.OnHoldEvent -= HandleHoldEvent;
            Player.InputSO.OnMoveEvent -= HandleMoveAnimation;
        }

        private void InitState()
        {
            foreach (var state in Enum.GetValues(typeof(PlayerState))) 
            {
                stateHash.Add((PlayerState)state, Animator.StringToHash(state.ToString()));
            }
        }

        private void HandleMoveAnimation(Vector2 vector)
        {
            if (vector.sqrMagnitude > 0)
                ChangeState(PlayerState.Walk);
            else
                ChangeState(PlayerState.Idle);
        }

        private void HandleHoldEvent(bool isTrue)
        {
            if (isTrue) ChangeState(PlayerState.Interact);
            else ChangeState(PlayerState.Idle);
        }

        private void ChangeState(PlayerState state) 
        {
            AnimCompo.SetBool(stateHash[curState], false);
            curState = state;
            AnimCompo.SetBool(stateHash[curState], true);
        }

        
        
    }

    public enum PlayerState
    {
        Idle,
        Walk,
        Interact,
        Hit,
        Dead,
    }
}
