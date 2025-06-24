using DG.Tweening;
using JMT.Agent;
using JMT.DayTime;
using JMT.Planets.Tile;
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
        private bool _isGaugeFull = false;
        
        public PVCUI PVCUI => pvcUI;
        public event Action OnGaugeFull;
        public event Action<bool> OnGaugeHold;
        private TimeData _buildTime;

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
            if (GameUIManager.Instance != null)
                GameUIManager.Instance.InteractCompo.OnHoldEvent -= HandleHoldEvent;
        }

        private void HandleHoldEvent(bool isHold)
        {
            var curTile = TileManager.Instance.CurrentTile;
            var myTile = GetComponentInParent<PlanetTile>();
            if (curTile == myTile)
            {
                _isHold = isHold;
                OnGaugeHold?.Invoke(isHold);
                AgentManager.Instance.Player.AnimatorCompo.SetLayer(2, 1);
            }
        }
        
        private void Update()
        {
            if (_isGaugeFull) return;
            if (_isHold)
            {
                _progressTime += Time.deltaTime / _buildTime.GetSecond();
                if (fillBarUI != null)
                {
                    fillBarUI.ResetBar(_progressTime);
                }

                if (fillBarUI.IsFull())
                {
                    _isGaugeFull = true;
                    OnGaugeFull?.Invoke();
                }
            }
            else
            {
                _progressTime -= Time.deltaTime / _buildTime.GetSecond();
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
            fillBarUI.ResetBar(0);
            _progressTime = 0;
            _buildTime = timeData;
        }

        public void PlayAnimation()
        {
            _dustEffect.Play();
            pvcUI.ActiveFillUI(false);
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
