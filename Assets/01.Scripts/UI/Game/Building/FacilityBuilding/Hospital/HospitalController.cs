using JMT.Agent;
using JMT.Agent.NPC;
using JMT.Building.Component;
using JMT.Core.Manager;
using JMT.Planets.Tile;
using JMT.UISystem.DayTime;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JMT.UISystem.Hospital
{
    public class HospitalController : MonoBehaviour
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


        private PanelUI currentPanel;

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

        public void OpenPanel()
        {
            view.OpenUI();
            GameUIManager.Instance.GameUICompo.CloseUI();
            GameUIManager.Instance.PlayerControlActive(false);
            SetCurrentPanel(workerView);
        }

        public void ClosePanel()
        {
            GameUIManager.Instance.GameUICompo.OpenUI();
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
            selectView.OpenUI();
            selectView.SetWorkerContent(AgentManager.Instance.UnemployedAgents);
        }

        private void HandleHireEvent(NPCAgent agent)
        {
            if (agent == null) return;

            Debug.Log("여기 수정해주세요.");
            var lodgingBuilding = BuildingManager.Instance.LodgingBuildings[Random.Range(0, BuildingManager.Instance.LodgingBuildings.Count)];
            if (lodgingBuilding == null) return;
            var spawnPos = lodgingBuilding.transform.position;

            AgentManager.Instance.SpawnNpc(spawnPos, Quaternion.identity);
            TileManager.Instance.CurrentTile.CurrentBuilding.GetBuildingComponent<BuildingNPC>().AddNpc(agent);
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

        public void SetCurrentPanel(PanelUI panel)
        {
            if (currentPanel == panel) return;
            selectView.CloseUI();
            statView.CloseUI();
            currentPanel?.CloseUI();
            currentPanel = panel;
            currentPanel?.OpenUI();
        }
    }
}
