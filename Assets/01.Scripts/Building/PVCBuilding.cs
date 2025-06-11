using DG.Tweening;
using JMT.DayTime;
using JMT.UISystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT
{
    public class PVCBuilding : MonoBehaviour
    {
        [SerializeField] private GameObject pvcObject;
        [SerializeField] private List<Transform> _walls;
        [SerializeField] private ParticleSystem _dustEffect;
        [SerializeField] private FillBarUI fillBarUI;
        [SerializeField] private PVCUI pvcUI;

        private float _progressTime = 0;
        private bool _isHold = false;

        private void Awake()
        {
            fillBarUI ??= GetComponent<FillBarUI>();
            pvcUI ??= GetComponent<PVCUI>();
        }

        private void Start()
        {
            GameUIManager.Instance.InteractCompo.OnHoldEvent += HandleHoldEvent;
        }

        private void OnDestroy()
        {
            GameUIManager.Instance.InteractCompo.OnHoldEvent -= HandleHoldEvent;
        }

        private void HandleHoldEvent(bool isHold)
        {
            _isHold = isHold;
        }
        
        private void Update()
        {
            if (_isHold)
            {
                _progressTime += Time.deltaTime;
                if (fillBarUI != null)
                {
                    fillBarUI.ResetBar(_progressTime);
                }
            }
            else
            {
                _progressTime -= Time.deltaTime;
                if (_progressTime < 0)
                {
                    _progressTime = 0;
                }
                if (fillBarUI != null)
                {
                    fillBarUI.ResetBar(_progressTime);
                }
            }
        }

        public void SetBuildTime(TimeData timeData)
        {
            SetVisualActive(true);
            Debug.Log(timeData);
            int secTime = timeData.GetSecond();
            Debug.Log(fillBarUI == null);
            fillBarUI.ResetBar(0);
            _progressTime = 0;
            //fillBarUI.SetHpBar(1, 1, secTime);
            //pvcUI.SetTime(secTime);
        }

        public void PlayAnimation()
        {
            _dustEffect.Play();
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
        
        public void SetVisualActive(bool isActive)
        {
            pvcObject.SetActive(isActive);
        }
    }
}
