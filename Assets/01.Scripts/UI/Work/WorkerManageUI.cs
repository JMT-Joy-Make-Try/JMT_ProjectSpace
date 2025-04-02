using DG.Tweening;
using JMT.Agent;
using JMT.Planets.Tile;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class WorkerManageUI : MonoBehaviour
    {
        private Transform workValue;
        private Image workValueIcon;
        private TextMeshProUGUI completeText, workValueText;
        private Button quitButton, hireButton;
        private CanvasGroup lockArea;

        private void Awake()
        {
            Transform work = transform.Find("Work");
            workValue = work.Find("WorkValue");
            workValueIcon = workValue.Find("Icon").GetComponent<Image>();
            workValueText = workValue.Find("ValueTxt").GetComponent<TextMeshProUGUI>();
            completeText = work.Find("Complete").GetComponentInChildren<TextMeshProUGUI>();
            quitButton = work.Find("QuitBtn").GetComponent<Button>();

            lockArea = transform.Find("Lock").GetComponent<CanvasGroup>();
            hireButton = lockArea.GetComponentInChildren<Button>();

            quitButton.onClick.AddListener(HandleQuitButton);
            hireButton.onClick.AddListener(HandleHireButton);
        }

        private void HandleQuitButton()
        {
            // 퇴사시키기 버튼
            ActiveLockArea(true);
            TileManager.Instance.CurrentTile.CurrentBuilding.RemoveNpc();
        }

        private void HandleHireButton()
        {
            // 고용하기 버튼
            ActiveLockArea(false);
            var npc = AgentManager.Instance.GetAgent();
            TileManager.Instance.CurrentTile.CurrentBuilding.AddNpc(npc);
        }

        private void ActiveLockArea(bool isActive)
        {
            lockArea.DOFade(isActive ? 1 : 0, 0.3f).SetUpdate(true);
            lockArea.interactable = isActive;
            lockArea.blocksRaycasts = isActive;
        }
    }
}
