using System;
using UnityEngine;

namespace JMT.UISystem.Rocket
{
    public class RocketController : MonoBehaviour
    {
        [SerializeField] private RocketView view;

        private void Awake()
        {
            view.OnUpgradeEvent += HandleUpgradeEvent;
            view.OnExitEvent += ClosePanel;
        }


        private void OnDestroy()
        {
            view.OnUpgradeEvent -= HandleUpgradeEvent;
            view.OnExitEvent -= ClosePanel;
        }

        public void OpenPanel()
        {
            view.OpenUI();
            GameUIManager.Instance.GameUICompo.ClosePanel();
            GameUIManager.Instance.PlayerControlActive(false);
        }

        private void ClosePanel()
        {
            GameUIManager.Instance.GameUICompo.OpenPanel();
            GameUIManager.Instance.PlayerControlActive(true);
            view.CloseUI();
        }

        private void HandleUpgradeEvent()
        {
            // 업그레이드 버튼을 눌렀을 때 해야할 기능
        }
    }
}
