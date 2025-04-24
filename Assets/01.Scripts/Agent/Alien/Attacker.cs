using JMT.Core;
using UnityEngine;

namespace JMT.Agent.Alien
{
    public class Attacker : MonoBehaviour
    {
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public int AttackDamage { get; private set; }
        [field: SerializeField] public LayerMask WhatIsAttackable { get; private set; }

        private Collider[] _colliders;
        

        private void Awake()
        {
            _colliders = new Collider[10];
        }

        public void Attack()
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position, AttackRange, _colliders, WhatIsAttackable);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (_colliders[i].TryGetComponent(out IDamageable damageable))
                    {
                        damageable.TakeDamage(AttackDamage);
                    }

                    if (_colliders[i].TryGetComponent(out IKnockbackable knockbackable))
                    {
                        Vector3 direction = (_colliders[i].transform.position - transform.position).normalized;
                        knockbackable.Knockback(direction, AttackDamage);
                    }

                    if (_colliders[i].TryGetComponent(out IStunable stunable))
                    {
                        stunable.Stun(1f);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}