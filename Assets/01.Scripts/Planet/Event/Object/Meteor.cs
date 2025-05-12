using DG.Tweening;
using JMT.Android.Vibration;
using JMT.Core;
using UnityEngine;

namespace JMT.Object
{
    public class Meteor : MonoBehaviour
    {
        [SerializeField] private LayerMask layer;
        [SerializeField] private int damage = 10;
        private Transform childTrm;     
        
        private CollisionDetector _detector;

        private void Awake()
        {
            childTrm = transform.GetChild(0);
            _detector = childTrm.GetComponent<CollisionDetector>();
            VibrationUtil.Vibrate(VibrationType.Pop);
            
            _detector.HandleTriggerEnter += HandleTriggerEnter;
        }
        
        private void OnDestroy()
        {
            if (_detector != null)
                _detector.HandleTriggerEnter -= HandleTriggerEnter;
        }

        private void Start()
        {
            childTrm.DOLocalMove(Vector3.zero, 5f).SetEase(Ease.Linear);
        }

        private void HandleTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                if ((layer & (1 << other.gameObject.layer)) != 0)
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
    }
}
