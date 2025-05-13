using System;
using UnityEngine;

namespace JMT.Object
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private bool _isTrigger;
        
        public event Action<Collision> HandleCollisionEnter;
        
        public event Action<Collider> HandleTriggerEnter;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_isTrigger)
            {
                Debug.Log("Trigger Enter");
                HandleTriggerEnter?.Invoke(other);
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (!_isTrigger)
            {
                HandleCollisionEnter?.Invoke(collision);
            }
        }
    }
}
