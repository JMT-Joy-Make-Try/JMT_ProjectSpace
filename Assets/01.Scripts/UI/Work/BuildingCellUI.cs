using JMT.Building;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JMT.UISystem
{
    public enum WorkType
    {
        Create, // 재료 아예 생성하는 거
        Change, // 재료를 딴 재료로 바꾸는 거
    }
    public class BuildingCellUI : MonoBehaviour, IPointerClickHandler
    {
        private TextMeshProUGUI buildingNameText;
        private WorkStatusUI workStatusUI;
        private WorkerListUI workerListUI;

        private void Awake()
        {
            buildingNameText = transform.Find("BuildingNameTxt").GetComponent<TextMeshProUGUI>();
            workStatusUI = transform.Find("WorkStatus").GetComponent<WorkStatusUI>();
            workerListUI = transform.Find("WorkerList").GetComponent<WorkerListUI>();
        }

        public void Init(BuildingDataSO buildingDataSO)
        {
            // 건물 이름이랑 로동자 / 작업현황 업데이트 해주는거
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //UIManager.Instance.WorkUI.CloseUI();
            // 구현 간바레
        }
    }
}
