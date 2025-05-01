using DG.Tweening;
using JMT.Agent;
using JMT.Building.Component;
using JMT.Core.Manager;
using JMT.Planets.Tile;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class WorkerManageUI : MonoBehaviour
    {
        private Image workValueIcon;
        private TextMeshProUGUI completeText, workValueText;
        private CellUI workValueCell;
        private Button quitButton, hireButton;
        private CanvasGroup lockArea;

        private void Awake()
        {
            Transform work = transform.Find("Work");
            workValueCell = work.Find("WorkValue").GetComponent<CellUI>();
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
            TileManager.Instance.CurrentTile.CurrentBuilding.GetBuildingComponent<BuildingNPC>().RemoveNpc();
        }

        private void HandleHireButton()
        {
            // 고용하기 버튼
            var npc = AgentManager.Instance.GetAgent();
            if (npc == null)
            {
                GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("일꾼이 부족합니다.");
                return;
            }

            var lodgingBuilding = BuildingManager.Instance.LodgingBuilding;
            if (lodgingBuilding == null) return;
            var spawnPos = lodgingBuilding.transform.position;
            AgentManager.Instance.SpawnNpc(spawnPos, Quaternion.identity);
            TileManager.Instance.CurrentTile.CurrentBuilding.GetBuildingComponent<BuildingNPC>().AddNpc(npc);

            ActiveLockArea(false);
        }

        public void ActiveLockArea(bool isActive)
        {
            lockArea.DOFade(isActive ? 1 : 0, 0.3f).SetUpdate(true);
            lockArea.interactable = isActive;
            lockArea.blocksRaycasts = isActive;
        }
    }
}
