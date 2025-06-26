using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using JMT.Core.Manager;
using JMT.Core.Tool;
using JMT.UISystem;
using UnityEngine;
using JMT.Sound;
using JMT.Agent;
using JMT.Core;
using JMT.DayTime;
using JMT.Effect;
using JMT.Planets.Tile;
using JMT.UISystem.Interact;
using System;

namespace JMT.PlayerCharacter
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private PlayerInputSO inputSO;
        [SerializeField] private LayerMask groundLayer;

        public PlayerHealth HealthCompo { get; private set; }
        public PlayerInventory InventoryCompo { get; private set; }
        public PlayerAnimator AnimatorCompo { get; private set; }
        public PlayerMovement MovementCompo { get; private set; }
        public PlayerTool PlayerToolCompo { get; private set; }
        public PlayerTileFinding TileFindingCompo { get; private set; }
        public FogDetect FogDetect { get; private set; }
        public Transform VisualTrm { get; private set; }
        public SoundPlayer SoundPlayer { get; private set; }
        public PlayerStat StatCompo { get; private set; }
        public EffectPlayer EffectCompo { get; private set; }
        public PlayerEffect PlayerEffectCompo { get; private set; }
        
        public PlayerInputSO InputSO => inputSO;
        public LayerMask GroundLayer => groundLayer;
        
        private bool isOxygenArea;
        private bool isTimeChanged;
        
        private List<Vignette> _vignetteList = new();
        private List<Color> _vignetteColorList = new();
        
        private void Awake()
        {
            VisualTrm = transform.Find("Visual");
            
            
            HealthCompo = GetComponent<PlayerHealth>();
            InventoryCompo = GetComponent<PlayerInventory>();
            PlayerToolCompo = GetComponent<PlayerTool>();
            AnimatorCompo = GetComponent<PlayerAnimator>();
            MovementCompo = GetComponent<PlayerMovement>();
            TileFindingCompo = GetComponent<PlayerTileFinding>();
            StatCompo = GetComponent<PlayerStat>();
            EffectCompo = GetComponent<EffectPlayer>();
            PlayerEffectCompo = GetComponent<PlayerEffect>();
            
            
            FogDetect = GetComponent<FogDetect>();
            SoundPlayer = GetComponentInChildren<SoundPlayer>();
            

            GameUIManager.Instance.TimeCompo.OnChangeTimeEvent += HandleChangeTimeEvent;
            GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent += HandleNightEvent;
            GameUIManager.Instance.InteractCompo.OnHoldEvent += HandleItem;
            InputSO.OnMoveEvent += HandleMoveEffect;
            HealthCompo.OnDamageEvent += HandleDamaged;
        }

        private void Start()
        {
            HealthCompo.Init(this);
            PlayerToolCompo.Init(this);
            InventoryCompo.Init(this);
            AnimatorCompo.Init(this);
            MovementCompo.Init(this);
            FogDetect.Init(this);
            TileFindingCompo.Init(this);
            StatCompo.Init(this);
            PlayerEffectCompo?.Init(this);
        }

        private void OnDestroy()
        {
            HealthCompo.OnDamageEvent -= HandleDamaged;
            InputSO.OnMoveEvent -= HandleMoveEffect;
            if (GameUIManager.Instance == null) return;
            if (GameUIManager.Instance.TimeCompo == null) return;
            GameUIManager.Instance.TimeCompo.OnChangeTimeEvent -= HandleChangeTimeEvent;
            GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent -= HandleNightEvent;
            GameUIManager.Instance.InteractCompo.OnHoldEvent -= HandleItem;
        }

        private void HandleItem(bool isHold)
        {
            var item = TileManager.Instance.CurrentTile.TileInteraction.GetItemType();
            if (isHold)
            {
                if (PlayerToolCompo.IsEquippedTool(PlayerToolType.Vacuum))
                {
                    PlayerEffectCompo?.PlayEffect("PlayerDust");
                    return;
                }
                if (PlayerToolCompo.IsEquippedTool(PlayerToolType.FuelDropper))
                {
                    PlayerEffectCompo?.PlayEffect("PlayerFuel");
                    return;
                }
                PlayerEffectCompo?.PlayEffect(item.ToString());
            }
            else
            {
                PlayerEffectCompo?.StopEffect(item.ToString());
            }
            
        }

        private void HandleMoveEffect(Vector2 movement)
        {
            if (movement.sqrMagnitude > 0.01f)
            {
                EffectCompo.PlayEffect();
            }
            else
            {
                EffectCompo.StopEffect();
            }
        }

        private void HandleNightEvent(DaytimeType type)
        {
            if (type == DaytimeType.Night)
            {
                GameUIManager.Instance.PlayerControlActive(false);
                AnimatorCompo.ChangeState(PlayerState.Sleep);
            }
            else if (type == DaytimeType.Day)
            {
                GameUIManager.Instance.PlayerControlActive(true);
                AnimatorCompo.ChangeState(PlayerState.Idle);
            }
        }

        private void HandleDamaged(int cur, int max)
        {
            if (_vignetteList.Count <= 0)
            {
                _vignetteList = VolumeManager.Instance.GetAllVolume<Vignette>();
                _vignetteColorList.Clear();

                foreach (var vignette in _vignetteList)
                {
                    _vignetteColorList.Add(vignette.color.value);
                }
            }

            float percent = cur.GetPercent(max) / 100f;
            Color damageColor = Color.red;

            for (int i = 0; i < _vignetteList.Count; i++)
            {
                var vignette = _vignetteList[i];
                var originalColor = _vignetteColorList[i];

                vignette.color.value = Color.Lerp(damageColor, originalColor, percent);
            }
            
            SoundPlayer.PlaySound("Player_Damaged");
        }

        private void HandleChangeTimeEvent(int m, int s)
        {
            if (isOxygenArea) return;
            if (isTimeChanged)
            {
                HealthCompo.AddOxygen(-1 * HealthCompo.OxygenMultiplier);
                isTimeChanged = false;
            }
            else
                isTimeChanged = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }
    }
}
