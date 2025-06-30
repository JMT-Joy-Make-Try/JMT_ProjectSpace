using JMT.Item;
using JMT.PlayerCharacter;
using JMT.UISystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.UISystem
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private GameUIView view;
        [SerializeField] private PlayerTool toolCompo;

        private void Awake()
        {
            view.OnSelectToolEvent += HandleSelectToolEvent;
            toolCompo.OnAddToolEvent += SetTools;
        }

        private void OnDestroy()
        {
            view.OnSelectToolEvent -= HandleSelectToolEvent;
            toolCompo.OnAddToolEvent -= SetTools;
        }

        private void HandleSelectToolEvent(ToolSO tool, bool isEnabled)
        {
            if (isEnabled)
                toolCompo.SetCloth(tool.ToolType);
            else
                toolCompo.UnEquipTool(tool.ToolType);
        }

        public void OpenPanel()
        {
            view.OpenUI();
        }
        public void ClosePanel()
        {
            view.CloseUI();
        }

        public void SetTools()
        {
            view.SetTools(toolCompo.PlayerTools.Values.ToList());
        }
    }
}
