using JMT.Core;
using System;
using UnityEngine;

namespace JMT.Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerInputSO inputSO;
        [SerializeField] private LayerMask groundLayer;

        public Transform VisualTrm { get; private set; }
        public Transform CameraTrm { get; private set; }
        public Rigidbody RigidCompo {  get; private set; }
        public Animator AnimCompo { get; private set; }
        public PlayerInputSO InputSO => inputSO;
        public LayerMask GroundLayer => groundLayer;

        [field:SerializeField] public int Health { get; private set; }
        private int _curHealth;
        public event Action<int, int> OnDamageEvent;
        public event Action OnDeadEvent;
        private void Awake()
        {
            VisualTrm = transform.Find("Visual");
            CameraTrm = transform.Find("Camera");
            RigidCompo = GetComponent<Rigidbody>();
            AnimCompo = VisualTrm.GetComponent<Animator>();

            InitHealth();
        }

        public void InitHealth()
        {
            _curHealth = Health;
        }

        public void TakeDamage(int damage)
        {
            _curHealth -= damage;
            OnDamageEvent?.Invoke(_curHealth, Health);
            if (_curHealth <= 0)
            {
                Dead();
            }
        }

        public void Dead()
        {
            OnDeadEvent?.Invoke();
        }
    }
}
