using System.Collections.Generic;
using JMT.UISystem;
using UnityEngine;
using JMT.Agent;
using System;

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
        
        public void Init(IPlayer player)
        {
            Player = player as Player;
            AnimCompo = Player?.VisualTrm.GetComponent<Animator>();
            EndTrigger = Player?.VisualTrm.GetComponent<AnimationEndTrigger>();

            InitState();
        }

        private void Start()
        {
            Player.InputSO.OnMoveEvent += HandleMoveAnimation;
            GameUIManager.Instance.InteractCompo.OnHoldEvent += HandleHoldEvent;
        }

        private void OnDestroy()
        {
            Player.InputSO.OnMoveEvent -= HandleMoveAnimation;
            if (GameUIManager.Instance != null && GameUIManager.Instance.InteractCompo != null)
                GameUIManager.Instance.InteractCompo.OnHoldEvent -= HandleHoldEvent;
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
            ChangeState(vector.sqrMagnitude > 0 ? PlayerState.Walk : PlayerState.Idle);
        }

        private void HandleHoldEvent(bool isHold)
        {
            if (isHold) ChangeState(PlayerState.Interact);
            else ChangeState(PlayerState.Idle);
        }

        private void ChangeState(PlayerState state) 
        {
            AnimCompo.SetBool(stateHash[curState], false);
            curState = state;
            AnimCompo.SetBool(stateHash[curState], true);
        }

        public void SetBool(PlayerState stateName, bool value)
        {
            if (stateHash.TryGetValue(stateName, out int hash))
            {
                AnimCompo.SetBool(hash, value);
            }
            else
            {
                Debug.LogWarning($"State {curState} not found in stateHash.");
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeState(PlayerState.Sleep);
            }
        }
    }

    public enum PlayerState
    {
        Idle,
        Walk,
        Interact,
        Carring,
        Sleep,
        Hit,
        Dead,
    }
}
