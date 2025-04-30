using DG.Tweening;
using JMT.DayTime;
using JMT.UISystem;
using System.Collections.Generic;
using UnityEngine;

namespace JMT
{
    public class PVCBuilding : MonoBehaviour
    {
        [SerializeField] private List<Transform> _walls;
        private FillBarUI fillBarUI;
        private PVCUI pvcUI;

        private void Awake()
        {
            fillBarUI = GetComponent<FillBarUI>();
            pvcUI = GetComponent<PVCUI>();
        }

        public void SetBuildTime(TimeData timeData)
        {
            int secTime = timeData.GetSecond();
            fillBarUI.ResetBar(0);
            fillBarUI.SetHpBar(1, 1, secTime);
            pvcUI.SetTime(secTime);
        }

        public void PlayAnimation()
        {
            pvcUI.ActiveUI(false, false);
            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < _walls.Count; i++)
            {
                Vector3 localRotation = _walls[i].localRotation.eulerAngles;
                sequence.Join(_walls[i].transform.DORotate(new Vector3(0, localRotation.y, localRotation.z), 1f).SetEase(Ease.OutBounce));
            }
            sequence.AppendInterval(0.5f);
            sequence.Append(transform.DOMoveY(-10, 3f));

            sequence.OnComplete(() => Destroy(gameObject));

            sequence.Play();
        }
    }
}
