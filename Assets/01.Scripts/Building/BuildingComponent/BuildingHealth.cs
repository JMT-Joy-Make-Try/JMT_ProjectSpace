using JMT.Core;
using System;
using UnityEngine;

namespace JMT.Building.Component
{
    public class BuildingHealth : MonoBehaviour, IDamageable, IBuildingComponent
    {
        [field: SerializeField] public int Health { get; protected set; } = 100;
        
        protected event Action OnBuildingBroken;
        public BuildingBase Building { get; private set; }
        
        private int _curHealth;
        
        public void InitStat()
        {
            _curHealth = Health;
        }

        public void TakeDamage(int damage, bool isHeal = false)
        {
            _curHealth += isHeal ? damage : -damage;
            if (_curHealth <= 0)
            {
                Dead();
            }
        }

        public void Dead()
        {
            OnBuildingBroken?.Invoke();
        }

        public void Init(BuildingBase building)
        {
            Building = building;
        }
    }
}