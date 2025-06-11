using JMT.Agent;
using JMT.Agent.NPC;
using JMT.Building.Component;
using JMT.Planets.Tile;
using UnityEngine;

namespace JMT.UISystem.Hospital
{
    public class HospitalController : BuildingController
    {
        [SerializeField] private HospitalView view;
        [Header("Worker View")]
        [SerializeField] private HospitalWorkerView workerView;
        [SerializeField] private NPCContentUI workerContent;
        [SerializeField] private HospitalWorkerSelectView selectView;

        [Header("Patient View")]
        [SerializeField] private HospitalPatientView patientView;
        [SerializeField] private HospitalPatientStatView statView;

        [Header("Upgrade View")]
        [SerializeField] private HospitalUpgradeView upgradeView;

        private void Awake()
        {
            view.OnWorkerButtonEvent += HandleWorkerEvent;
            view.OnPatientButtonEvent += HandlePatientEvent;
            view.OnUpgradeButtonEvent += HandleUpgradeEvent;
            view.OnExitButtonEvent += ClosePanel;

            workerContent.OnAddEvent += HandleAddEvent;
            workerContent.OnQuitEvent += HandleQuitEvent;

            selectView.OnHireEvent += HandleHireEvent;

            patientView.OnClickPatientEvent += HandleClickPatientEvent;
        }


        private void OnDestroy()
        {
            view.OnWorkerButtonEvent -= HandleWorkerEvent;
            view.OnPatientButtonEvent -= HandlePatientEvent;
            view.OnUpgradeButtonEvent -= HandleUpgradeEvent;
            view.OnExitButtonEvent -= ClosePanel;
        }

        public override void OpenPanel()
        {
            view.OpenUI();
            GameUIManager.Instance.GameUICompo.ClosePanel();
            GameUIManager.Instance.PlayerControlActive(false);
            SetCurrentPanel(workerView);
        }

        public override void ClosePanel()
        {
            GameUIManager.Instance.GameUICompo.OpenPanel();
            GameUIManager.Instance.PlayerControlActive(true);
            view.CloseUI();
            SetCurrentPanel(null);
        }

        private void HandleWorkerEvent()
        {
            SetCurrentPanel(workerView);
        }

        private void HandlePatientEvent()
        {
            SetCurrentPanel(patientView);
        }

        private void HandleUpgradeEvent()
        {
            SetCurrentPanel(upgradeView);
        }

        private void HandleAddEvent()
        {
            // 고용하기 버튼

            if (selectView.IsOpen) return;

            selectView.OpenUI();
            selectView.SetWorkerContent(AgentManager.Instance.UnemployedAgents);
        }

        private void HandleHireEvent(NPCAgent agent)
        {
            if (agent == null) return;

            Debug.Log("여기 수정해주세요.");
            AgentManager.Instance.SpawnNpc(agent);
            selectView.CloseUI();
        }

        private void HandleQuitEvent()
        {
            workerContent.ActiveLockArea(true);
            TileManager.Instance.CurrentTile.CurrentBuilding.GetBuildingComponent<BuildingNPC>().RemoveNpc();
        }

        private void HandleClickPatientEvent(NPCAgent agent)
        {
            statView.OpenUI();
            statView.SetStatPanel(agent);
        }
    }
}
