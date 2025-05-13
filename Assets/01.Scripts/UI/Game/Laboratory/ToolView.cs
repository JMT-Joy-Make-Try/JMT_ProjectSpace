using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Laboratory
{
    public class ToolView : SidePanelUI
    {
        public Action OnItemCreateEvent;
        [SerializeField] private Transform needItemTrm;
        [SerializeField] private Transform toolTypeTrm;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descText;
        [SerializeField] private Button createButton;
        private List<CellUI> needItemList;
        private Image toolTypeIcon;

        private void Awake()
        {
            needItemList = needItemTrm.GetComponentsInChildren<CellUI>().ToList();
            toolTypeIcon = toolTypeTrm.Find("Icon").GetComponent<Image>();

            createButton.onClick.AddListener(HandleCreateButton);
        }

        private void OnDestroy()
        {
            createButton.onClick.RemoveListener(HandleCreateButton);
        }

        private void HandleCreateButton()
        {
            OnItemCreateEvent?.Invoke();
        }

        public void SetInfo(ToolSO tool)
        {
            nameText.text = tool.ItemName;
            descText.text = tool.ItemDescription;
            for(int i = 0; i <  needItemList.Count; i++)
            {
                var needItems = tool.NeedItems.ToList();
                needItemList[i].SetCell(needItems[i].Key, needItems[i].Value.ToString());
            }
        }
    }
}
