using JMT.Core;
using System;
using UnityEngine;

namespace JMT.Agent.Alien
{
    public class AlienAttacker : MonoBehaviour
    {
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public int AttackDamage { get; private set; }
        
        private Collider[] _colliders;

        private void Awake()
        {
            _colliders = new Collider[10];
        }

        public void Attack(AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.OneShot:
                    OneShotAttack();
                    break;
                case AttackType.Continuous:
                    ContinuousAttack();
                    break;
            }
        }

        private void ContinuousAttack()
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position, AttackRange, _colliders);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (_colliders[i].TryGetComponent(out IDamageable damageable))
                    {
                        damageable.TakeDamage(AttackDamage);
                    }
                }
            }
        }

        private void OneShotAttack()
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position, AttackRange, _colliders);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (_colliders[i].TryGetComponent(out IDamageable damageable))
                    {
                        damageable.TakeDamage(AttackDamage);
                    }
                }
            }
        }
    }
    
    public enum AttackType
    {
        OneShot,
        Continuous
    }
}