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
        public Animator ToolAnimCompo { get; private set; }
        public AnimationEndTrigger EndTrigger { get; private set; }
        public PlayerState CurrentState => curState;

        private Dictionary<PlayerState, int> stateHash;

        [SerializeField] private PlayerState curState;
        private PlayerState saveState;
        private float _coolTime = 0.4f;
        private int _currentLayer = 0;

        private void Awake()
        {
            stateHash = new Dictionary<PlayerState, int>();
        }

        public void Init(IPlayer player)
        {
            Player = player as Player;
            AnimCompo = Player?.VisualTrm.GetComponent<Animator>();
            EndTrigger = Player?.VisualTrm.GetComponent<AnimationEndTrigger>();
            ToolAnimCompo = Player?.PlayerToolCompo.CurrentCloth;

            Player.PlayerToolCompo.OnClothChange += HandleToolAnimChange;



            InitState();
        }

        private void Start()
        {
            Player.InputSO.OnMoveEvent += HandleMoveAnimation;
            GameUIManager.Instance.InteractCompo.OnHoldEvent += HandleHoldEvent;
            GameUIManager.Instance.InteractCompo.OnAnimationEndEvent += HandleAnimation;
        }



        private void OnDestroy()
        {
            Player.InputSO.OnMoveEvent -= HandleMoveAnimation;
            Player.PlayerToolCompo.OnClothChange -= HandleToolAnimChange;
            if (GameUIManager.Instance != null && GameUIManager.Instance.InteractCompo != null)
            {
                GameUIManager.Instance.InteractCompo.OnHoldEvent -= HandleHoldEvent;
                GameUIManager.Instance.InteractCompo.OnAnimationEndEvent -= HandleAnimation;
            }
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
            if (Player.TileFindingCompo.IsTileIsHold()) return;
            if (isHold) ChangeState(PlayerState.Interact);
            else ChangeState(PlayerState.Idle);
        }

        private void HandleAnimation()
        {
            ChangeState(PlayerState.Idle);
        }

        public void ChangeState(PlayerState state)
        {
            AnimCompo.SetBool(stateHash[curState], false);
            ToolAnimCompo?.SetBool(stateHash[curState], false);
            curState = state;
            AnimCompo.SetBool(stateHash[curState], true);
            ToolAnimCompo?.SetBool(stateHash[curState], true);
        }

        public void SetBool(PlayerState stateName, bool value)
        {
            if (stateHash.TryGetValue(stateName, out int hash))
            {
                AnimCompo.SetBool(hash, value);
                ToolAnimCompo?.SetBool(hash, value);
            }
            else
            {
                Debug.LogWarning($"State {curState} not found in stateHash.");
            }
        }

        public void SetLayer(PlayerAnimationLayer layer, float weight)
        {
            AnimCompo.SetLayerWeight(_currentLayer, 0);
            _currentLayer = (int)layer;
            AnimCompo.SetLayerWeight(_currentLayer, weight);
        }

        private void HandleToolAnimChange(Animator toolAnim)
        {
            ToolAnimCompo = toolAnim;
            if (ToolAnimCompo != null)
            {
                ToolAnimCompo.SetBool(stateHash[curState], true);
            }
        }
    }

    public enum PlayerState
    {
        Idle,
        Walk,
        Interact,
        Caring,
        Sleep,
        Hit,
        Dead,
    }

    public enum PlayerAnimationLayer
    {
        BaseLayer = 0,
        CarringLayer = 1,
        BuildLayer = 2,
        FieldLayer = 3,
        VacuumLayer = 4,
        ScannerLayer = 5,
        FuelLayer = 6,
    }
}
