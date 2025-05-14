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
        private int KnockbackDamage = 10;
        

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
                    CheckComponent(i);
                    CheckParentComponent(i);
                }
            }
        }

        private void CheckParentComponent(int index)
        {
            var parent = _colliders[index].transform.parent;
            if (parent == null) return;
            if (parent.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(AttackDamage);
            }

            if (parent.TryGetComponent(out IKnockbackable knockbackable))
            {
                Vector3 direction = (_colliders[index].transform.position - transform.position).normalized;
                knockbackable.Knockback(direction, KnockbackDamage);
            }

            if (parent.TryGetComponent(out IStunable stunable))
            {
                stunable.Stun(1f);
            }
        }

        private void CheckComponent(int index)
        {
            if (_colliders[index] == null) return;
            if (_colliders[index].TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(AttackDamage);
            }

            if (_colliders[index].TryGetComponent(out IKnockbackable knockbackable))
            {
                Vector3 direction = (_colliders[index].transform.position - transform.position).normalized;
                knockbackable.Knockback(direction, KnockbackDamage);
            }

            if (_colliders[index].TryGetComponent(out IStunable stunable))
            {
                stunable.Stun(1f);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}