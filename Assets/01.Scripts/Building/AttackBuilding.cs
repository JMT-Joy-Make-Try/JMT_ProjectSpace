using JMT.Agent.Alien;
using JMT.Object;
using System.Collections;
using UnityEngine;

namespace JMT.Building
{
    public class AttackBuilding : BuildingBase
    {
        [SerializeField] private AttackableObject _attackableObject;
        [SerializeField] private LayerMask _whatIsAttackable;
        [SerializeField] private float _attackRange;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _rotateTransform;
        [SerializeField] private Transform _attackPoint;
        
        private Collider[] _colliders;
        
        protected override void Awake()
        {
            base.Awake();
            _colliders = new Collider[10];
        }
        
        
        public override void Work()
        {
            base.Work();
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            while (_target != null)
            {
                _target = FindTarget();
                if (_target != null)
                {
                    if (Vector3.Distance(_target.position, transform.position) <= _attackRange)
                    {
                        _rotateTransform.LookAt(_target);
                        _attackableObject.Spawn(_attackPoint.position);
                    }
                }
                yield return new WaitForSeconds(1f);
            }
        }

        private Transform FindTarget()
        {
            int colliders = Physics.OverlapSphereNonAlloc(transform.position, _attackRange, _colliders, _whatIsAttackable);
            for (int i = 0; i < colliders; i++)
            {
                if (_colliders[i].TryGetComponent(out Alien alien))
                {
                    return alien.transform;
                }
            }

            return null;
        }
    }
}