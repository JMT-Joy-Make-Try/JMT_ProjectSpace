using JMT.Agent.NPC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem.Hospital
{
    public class HospitalWorkerSelectView : SidePanelUI
    {
        public event Action<NPCAgent> OnHireEvent;
        public List<Action> handlers = new();
        [SerializeField] private Transform workerContent;
        [SerializeField] private NPCContentUI workerContentPrefab;

        public void SetWorkerContent(List<NPCAgent> agents)
        {
            for(int i = 0; i < agents.Count; ++i)
            {
                int value = i;
                handlers.Add(() => OnHireEvent?.Invoke(agents[value]));
                NPCContentUI content = Instantiate(workerContentPrefab, workerContent);
                content.SetWorkerPanel(agents[value]);
                content.OnAddEvent += handlers[value];
            }
        }

        private void OnDestroy()
        {
            ResetWorkerContent();
        }

        public override void CloseUI()
        {
            base.CloseUI();
            ResetWorkerContent();
        }

        private void ResetWorkerContent()
        {
            for(int i = 0; i < workerContent.childCount; ++i)
            {
                int value = i;
                NPCContentUI content = workerContent.GetChild(value).GetComponent<NPCContentUI>();
                content.OnAddEvent -= handlers[value];
                Destroy(content.gameObject);
            }
        }
    }
}
