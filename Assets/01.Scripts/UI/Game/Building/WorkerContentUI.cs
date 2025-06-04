using DG.Tweening;
using JMT.Agent.NPC;
using JMT.Agent;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace JMT.UISystem
{
    public class WorkerContentUI : MonoBehaviour
    {
        public event Action OnAddEvent;
        public event Action OnQuitEvent;

        [Header("Health Settings")]
        [SerializeField] private List<Sprite> healthIcons;
        [SerializeField] private Image workerHealthImage;

        [Header("Work Settings")]
        [SerializeField] private CellUI workValueCell;
        [SerializeField] private TextMeshProUGUI completeText;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button hireButton;
        [SerializeField] private TextMeshProUGUI workerOxygenValueText;
        [SerializeField] private CanvasGroup lockArea;

        private void Awake()
        {
            hireButton.onClick.AddListener(HandleHireButton);
            quitButton.onClick.AddListener(HandleQuitButton);
        }

        private void OnDestroy()
        {
            hireButton.onClick.RemoveListener(HandleHireButton);
            quitButton.onClick.RemoveListener(HandleQuitButton);
        }

        private void HandleHireButton()
        {
            OnAddEvent?.Invoke();
        }

        private void HandleQuitButton()
        {
            OnQuitEvent?.Invoke();
        }

        public void SetWorkerPanel(NPCAgent npc)
        {
            NPCWorkData workData = npc.WorkData;
            NPCHealth healthData = npc.Health;
            NPCOxygen oxygenData = npc.OxygenCompo;
            // 몇 초 뒤에 완료 대충 이런텍스트 띄우는 친구
            if (completeText != null)
                completeText.text = workData.TimeData.GetTimeString();
            // workData.TimeData;로 시간 접근

            // 현재 제작하고 있는 아이템과 그 갯수
            workValueCell?.SetCell(workData.CurrentItem?.ResultItem, "X1");

            // 현재 NPC의 스탯(건강, 산소)

            // 0번 = 건강 좋음
            // 1번 = 건강 중간
            // 2번 = 건강 나쁨
            Debug.Log(healthData.GetStatus());
            workerHealthImage.sprite = healthIcons[healthData.GetStatus()];
            // 현재 산소
            if (workerOxygenValueText != null)
                workerOxygenValueText.text = oxygenData.Oxygen.ToString();
        }

        public void ActiveLockArea(bool isActive)
        {
            lockArea.DOFade(isActive ? 1 : 0, 0.3f).SetUpdate(true);
            lockArea.interactable = isActive;
            lockArea.blocksRaycasts = isActive;
        }
    }
}
