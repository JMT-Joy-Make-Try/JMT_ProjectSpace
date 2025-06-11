using UnityEngine;

namespace JMT.UISystem.Village
{
    public class VillageController : MonoBehaviour
    {
        [SerializeField] private VillageView view;
        private VillageSO villageSO;

        private void Awake()
        {
            view.OnAcceptEvent += HandleAcceptEvent;
            view.OnExitEvent += ClosePanel;
        }

        private void OnDestroy()
        {
            view.OnAcceptEvent -= HandleAcceptEvent;
            view.OnExitEvent -= ClosePanel;
        }

        public void OpenPanel(VillageSO villageSO)
        {
            this.villageSO = villageSO;
            view.SetVillagePanel(villageSO);
            view.OpenUI();
        }

        public void ClosePanel()
        {
            villageSO = null;
            view.CloseUI();
        }

        private void HandleAcceptEvent()
        {
            if (villageSO == null) return;

            Debug.Log("퀘스트 연결이 필요합니다.");
            villageSO.AddNpc();
            view.CloseUI();
        }
    }
}
