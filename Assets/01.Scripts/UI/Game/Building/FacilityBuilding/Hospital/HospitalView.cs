using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Hospital
{
    public class HospitalView : MonoBehaviour
    {
        public event Action OnWorkerButtonEvent;
        public event Action OnPatientButtonEvent;
        public event Action OnUpgradeButtonEvent;
        public event Action OnExitButtonEvent;

        [SerializeField] private Button workerButton, patientButton, upgradeButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            workerButton.onClick.AddListener(HandleWorkerButton);
            patientButton.onClick.AddListener(HandlePatientButton);
            upgradeButton.onClick.AddListener(HandleUpgradeButton);
            exitButton.onClick.AddListener(HandleExitButton);
        }

        private void OnDestroy()
        {
            workerButton.onClick.RemoveListener(HandleWorkerButton);
            patientButton.onClick.RemoveListener(HandlePatientButton);
            upgradeButton.onClick.RemoveListener(HandleUpgradeButton);
            exitButton.onClick.RemoveListener(HandleExitButton);
        }

        private void HandleWorkerButton()
        {
            OnWorkerButtonEvent?.Invoke();
        }

        private void HandlePatientButton()
        {
            OnPatientButtonEvent?.Invoke();
        }

        private void HandleUpgradeButton()
        {
            OnUpgradeButtonEvent?.Invoke();
        }

        private void HandleExitButton()
        {
            OnExitButtonEvent?.Invoke();
        }
    }
}
