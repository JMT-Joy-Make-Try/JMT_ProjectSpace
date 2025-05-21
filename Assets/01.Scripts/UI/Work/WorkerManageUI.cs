using DG.Tweening;
using JMT.Agent;
using JMT.Agent.NPC;
using JMT.Building.Component;
using JMT.Core.Manager;
using JMT.Planets.Tile;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class WorkerManageUI : MonoBehaviour
    {
        [SerializeField] private List<Sprite> healthIcons;
        [SerializeField] private Image workerHealthImage;
        [SerializeField] private TextMeshProUGUI workerOxygenValueText;
        [SerializeField] private TextMeshProUGUI completeText;
        [SerializeField] private CellUI workValueCell;
        [SerializeField] private Button quitButton;
        [SerializeField] private CanvasGroup lockArea;
        [SerializeField] private Button hireButton;

        private void Awake()
        {
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
            SetWorkerPanel(npc);
        }

        public void SetWorkerPanel(NPCAgent npc)
        {
            NPCWorkData workData = npc.WorkData;
            NPCHealth healthData = npc.Health;
            NPCOxygen oxygenData = npc.OxygenCompo;
            // 몇 초 뒤에 완료 대충 이런텍스트 띄우는 친구
            if(completeText != null)
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
