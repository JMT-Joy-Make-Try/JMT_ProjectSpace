using DG.Tweening;
using JMT.Android.Vibration;
using JMT.PlayerCharacter;
using System.Collections;
using UnityEngine;

namespace JMT.Object
{
    public class Meteor : MonoBehaviour
    {
        [SerializeField] private LayerMask layer;
        private Transform childTrm;
        private Collider[] col = new Collider[10];

        private void Awake()
        {
            childTrm = transform.GetChild(0);
            StartCoroutine(MeteorRoutine());
            
            VibrationUtil.Vibrate(VibrationType.Pop);
        }

        private IEnumerator MeteorRoutine()
        {
            childTrm.DOLocalMove(Vector3.zero, 3f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(3f);
            Physics.OverlapSphereNonAlloc(childTrm.position, 4f, col, layer);
            /*col[0].TryGetComponent<Player>(out Player player) {

            }*/
            Destroy(gameObject, 3f);
        }
    }
}
