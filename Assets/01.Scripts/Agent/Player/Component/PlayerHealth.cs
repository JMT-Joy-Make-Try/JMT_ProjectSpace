using JMT.Core;
using System;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerHealth : MonoBehaviour, IPlayerComponent, IDamageable, IOxygen
    {
        public Player Player { get; private set; }
        
        public event Action<int, int> OnOxygenEvent;
        public event Action<int, int> OnDamageEvent;
        public event Action OnDeadEvent;

        [field:SerializeField] public int Health { get; private set; }
        private int _curHealth;
        
        [field:SerializeField] public int Oxygen { get; private set; }
        private int _curOxygen;
        public int OxygenMultiplier { get; private set; } = 1;
        
        private bool _isDead;
        
        public void Init(Player player)
        {
            Player = player;
            InitStat();
        }
        
        public void InitStat()
        {
            _curHealth = Health;
            _curOxygen = Oxygen;
            _isDead = false;
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
        
        public void Dead()
        {
            OnDeadEvent?.Invoke();
            _isDead = true;
        }
        
        public void AddOxygen(int value)
        {
            _curOxygen += value;
            _curOxygen = Mathf.Clamp(_curOxygen, 0, Oxygen);
            OnOxygenEvent?.Invoke(_curOxygen, Oxygen);
        }
        
        public void SetOxygenMultiplier(int multiplier)
        {
            OxygenMultiplier = multiplier;
        }
    }
}