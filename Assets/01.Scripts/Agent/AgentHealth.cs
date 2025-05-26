using JMT.Core;
using JMT.UISystem;
using System;
using UnityEngine;

namespace JMT.Agent
{
    public class AgentHealth : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public int Health { get; set; } = 100;
        public bool IsDead { get; private set; }
        
        protected int _curHealth;
        private FillBarUI hpFillBarUI;
        
        public int CurHealth => _curHealth;
        
        public event Action OnDeath;

        private void Awake()
        {
            hpFillBarUI = GetComponent<FillBarUI>();
        }

        public void InitStat()
        {
            _curHealth = Health;
            IsDead = false;
            hpFillBarUI?.ResetBar(Health);
        }

        public virtual void TakeDamage(int damage, bool isHeal)
        {
            _curHealth += isHeal ? damage : -damage;
            hpFillBarUI?.SetHpBar(_curHealth, Health);
            if (_curHealth <= 0)
            {
                Dead();
            }
        }

        public void Dead()
        {
            if (IsDead) return;
            IsDead = true;
            OnDeath?.Invoke();
            Debug.Log(gameObject.name + " is dead");
        }
    }
}