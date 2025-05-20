using JMT.Agent;
using JMT.Agent.Alien;
using JMT.Core;
using JMT.Core.Manager;
using JMT.Core.Tool;
using JMT.Sound;
using JMT.UISystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace JMT.PlayerCharacter
{
    public class Player : MonoBehaviour, IDamageable, IOxygen
    {
        [SerializeField] private PlayerInputSO inputSO;
        [SerializeField] private LayerMask groundLayer;

        public event Action<int, int> OnDamageEvent;
        public event Action<int, int> OnOxygenEvent;
        public event Action OnDeadEvent;

        public Transform VisualTrm { get; private set; }
        public Transform CameraTrm { get; private set; }
        public Rigidbody RigidCompo { get; private set; }
        public Animator AnimCompo { get; private set; }
        public AnimationEndTrigger EndTrigger { get; private set; }
        public Attacker Attacker { get; private set; }
        public PlayerMovement Movement { get; private set; }
        public PlayerInputSO InputSO => inputSO;
        public FogDetect FogDetect { get; private set; }
        public PlayerTool PlayerTool { get; private set; }
        public SoundPlayer SoundPlayer { get; private set; }
        public LayerMask GroundLayer => groundLayer;

        [field:SerializeField] public int Health { get; private set; }
        [field:SerializeField] public int Oxygen { get; private set; }
        
        public int OxygenMultiplier { get; private set; } = 1;

        private int _curHealth;
        private int _curOxygen;
        private bool isOxygenArea;
        private bool isTimeChanged;
        
        private bool _isDead;
        
        private List<Vignette> _vignetteList = new();
        private List<Color> _vignetteColorList = new();
        
        private void Awake()
        {
            VisualTrm = transform.Find("Visual");
            CameraTrm = transform.Find("Camera");
            RigidCompo = GetComponent<Rigidbody>();
            Attacker = GetComponent<Attacker>();
            AnimCompo = VisualTrm.GetComponent<Animator>();
            EndTrigger = VisualTrm.GetComponent<AnimationEndTrigger>();
            Movement = GetComponent<PlayerMovement>();
            FogDetect = GetComponent<FogDetect>();
            PlayerTool = GetComponent<PlayerTool>();
            SoundPlayer = GetComponentInChildren<SoundPlayer>();

            GameUIManager.Instance.TimeCompo.OnChangeTimeEvent += HandleChangeTimeEvent;
            OnDamageEvent += HandleDamaged;

            InitStat();
            FogDetect.Init(this);
            PlayerTool.Init(this);
        }

        private void OnDestroy()
        {
            if (GameUIManager.Instance == null) return;
            if (GameUIManager.Instance.TimeCompo == null) return;
            GameUIManager.Instance.TimeCompo.OnChangeTimeEvent -= HandleChangeTimeEvent;
            OnDamageEvent -= HandleDamaged;
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

        public void InitStat()
        {
            _curHealth = Health;
            _curOxygen = Oxygen;
        }

        private void HandleChangeTimeEvent(int m, int s)
        {
            if (isOxygenArea) return;
            if (isTimeChanged)
            {
                AddOxygen(-1 * OxygenMultiplier);
                isTimeChanged = false;
            }
            else
                isTimeChanged = true;
        }

        public void TakeDamage(int damage, bool isHeal = false)
        {
            if (_isDead) return;
            _curHealth += isHeal ? damage : -damage;
            OnDamageEvent?.Invoke(_curHealth, Health);
            if (_curHealth <= 0)
            {
                Dead();
            }
        }

        public void AddOxygen(int value)
        {
            _curOxygen += value;
            _curOxygen = Mathf.Clamp(_curOxygen, 0, Oxygen);
            OnOxygenEvent?.Invoke(_curOxygen, Oxygen);
        }

        public void Dead()
        {
            OnDeadEvent?.Invoke();
            _isDead = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }
        
        public void SetOxygenMultiplier(int multiplier)
        {
            OxygenMultiplier = multiplier;
        }
    }
}
