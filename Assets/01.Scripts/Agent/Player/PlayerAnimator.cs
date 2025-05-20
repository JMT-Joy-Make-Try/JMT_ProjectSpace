using JMT.UISystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Player player;

        private Dictionary<PlayerState, int> stateHash;

        [SerializeField] private PlayerState curState;
        private PlayerState saveState;
        private bool isAttack, canAttack = true;
        private float coolTime = 0.4f;

        private void Awake()
        {
            player = GetComponent<Player>();
            
            stateHash = new Dictionary<PlayerState, int>();

            InitState();
        }

        private void Start()
        {
            player.InputSO.OnMoveEvent += HandleMoveAnimation;
            GameUIManager.Instance.InteractCompo.OnHoldEvent += HandleHoldEvent;
            GameUIManager.Instance.InteractCompo.OnAttackEvent += HandleChangeInteractEvent;
            player.EndTrigger.OnAnimationEnd += HandleAnimationEndEvent;
        }

        private void HandleAnimationEndEvent()
        {
            if (isAttack && !canAttack)
            {
                StartCoroutine(AttackCoolTime());
            }
        }

        private IEnumerator AttackCoolTime()
        {
            isAttack = false;
            ChangeState(saveState);
            yield return new WaitForSeconds(coolTime);
            canAttack = true;
            ChangeState(saveState);
        }

        private void HandleChangeInteractEvent()
        {
            if (!canAttack) return;
            canAttack = false;
            isAttack = true;
            saveState = curState;
            player.Attacker.Attack();
            player.SoundPlayer.PlaySound("Player_Attack");
            ChangeState(PlayerState.Attack);
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
            if (isAttack && !canAttack)
            {
                if (vector.sqrMagnitude > 0)
                    saveState = PlayerState.Walk;
                else
                    saveState = PlayerState.Idle;
                return;
            }
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
        Attack,
        Hit,
        Dead,
    }
}
