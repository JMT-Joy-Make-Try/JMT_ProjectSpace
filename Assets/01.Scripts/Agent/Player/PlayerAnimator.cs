using AYellowpaper.SerializedCollections;
using JMT.UISystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Player player;

        private Dictionary<PlayerState, int> stateHash;

        [SerializeField] private PlayerState curState;

        private void Awake()
        {
            player = GetComponent<Player>();
            player.InputSO.OnMoveEvent += HandleMoveAnimation;
            UIManager.Instance.GameUI.OnHoldEvent += HandleHoldEvent;
            stateHash = new Dictionary<PlayerState, int>();

            InitState();
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
            player.AnimCompo.SetBool(stateHash[curState], false);
            curState = state;
            player.AnimCompo.SetBool(stateHash[curState], true);
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
