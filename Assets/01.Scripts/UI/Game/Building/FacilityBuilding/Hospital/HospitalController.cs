using JMT.UISystem.DayTime;
using UnityEngine;

namespace JMT.UISystem.Hospital
{
    public class HospitalController : MonoBehaviour
    {
        [SerializeField] private HospitalView view;
        [SerializeField] private HospitalWorkerView workerView;
        [SerializeField] private HospitalPatientView patientView;
        [SerializeField] private HospitalUpgradeView upgradeView;

        private PanelUI currentPanel;

        private void Awake()
        {
            view.OnWorkerButtonEvent += HandleWorkerEvent;
            view.OnPatientButtonEvent += HandlePatientEvent;
            view.OnUpgradeButtonEvent += HandleUpgradeEvent;
            view.OnExitButtonEvent += ClosePanel;
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

        public void SetCurrentPanel(PanelUI panel)
        {
            if (currentPanel == panel) return;
            //sideView.CloseUI();
            currentPanel?.CloseUI();
            currentPanel = panel;
            currentPanel?.OpenUI();
        }
    }
}
