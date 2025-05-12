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

        private void Awake()
        {
            childTrm = transform.GetChild(0);
            VibrationUtil.Vibrate(VibrationType.Pop);
        }

        private void Start()
        {
            childTrm.DOLocalMove(Vector3.zero, 3f).SetEase(Ease.Linear);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                if (other.gameObject.layer == layer)
                    damageable.TakeDamage(damage);
            }
        }
    }
}
