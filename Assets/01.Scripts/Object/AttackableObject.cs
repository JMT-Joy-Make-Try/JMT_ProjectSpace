using JMT.Core;
using System;
using UnityEngine;

namespace JMT.Object
{
    public class AttackableObject : MonoBehaviour, ISpawnable
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
        
        private void Start()
        {
            Destroy(gameObject, LifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(Damage);
            }
        }

        public GameObject Spawn(Vector3 position)
        {
            var gameObj = Instantiate(gameObject, position, Quaternion.identity);
            return gameObj;
        }
    }
}