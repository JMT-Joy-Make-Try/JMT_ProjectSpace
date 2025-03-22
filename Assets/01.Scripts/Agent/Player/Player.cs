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

        public int Health { get; }
        private int _curHealth;
        public event Action OnDamage;
        private void Awake()
        {
            VisualTrm = transform.Find("Visual");
            CameraTrm = transform.Find("Camera");
            RigidCompo = GetComponent<Rigidbody>();
            AnimCompo = VisualTrm.GetComponent<Animator>();
        }

        public void InitHealth()
        {
            _curHealth = Health;    
        }

        public void TakeDamage(int damage)
        {
            _curHealth -= damage;
            if (_curHealth <= 0)
            {
                Dead();
            }
        }

        public void Dead()
        {
            OnDamage?.Invoke();
        }
    }
}
