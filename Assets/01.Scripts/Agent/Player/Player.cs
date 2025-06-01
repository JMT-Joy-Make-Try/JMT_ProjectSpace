using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using JMT.Core.Manager;
using JMT.Core.Tool;
using JMT.UISystem;
using UnityEngine;
using JMT.Sound;
using JMT.Agent;
using JMT.Core;

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
        public FogDetect FogDetect { get; private set; }
        public Transform VisualTrm { get; private set; }
        public SoundPlayer SoundPlayer { get; private set; }
        
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
            AnimatorCompo = GetComponent<PlayerAnimator>();
            MovementCompo = GetComponent<PlayerMovement>();
            PlayerToolCompo = GetComponent<PlayerTool>();
            
            
            FogDetect = GetComponent<FogDetect>();
            SoundPlayer = GetComponentInChildren<SoundPlayer>();
            

            GameUIManager.Instance.TimeCompo.OnChangeTimeEvent += HandleChangeTimeEvent;
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
        }

        private void OnDestroy()
        {
            if (GameUIManager.Instance == null) return;
            if (GameUIManager.Instance.TimeCompo == null) return;
            GameUIManager.Instance.TimeCompo.OnChangeTimeEvent -= HandleChangeTimeEvent;
            HealthCompo.OnDamageEvent -= HandleDamaged;
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
