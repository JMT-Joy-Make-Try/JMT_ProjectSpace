using JMT.Agent.NPC;
using JMT.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.UISystem.Building
{
    public class ManageView : PanelUI
    {
        [SerializeField] private Transform workerManageContent;
        [SerializeField] private Transform createItemContent;
        [SerializeField] private CellUI cellPrefab;
        private List<WorkerManageUI> workers;
        

        private void Awake()
        {
            workers = workerManageContent.GetComponentsInChildren<WorkerManageUI>().ToList();
        }

        public void SetWorkers(List<NPCAgent> npcAgents)
        {
            for(int i = 0; i < workers.Count; i++)
            {
                bool isTrue =  i < npcAgents.Count;
                Debug.Log("Workers isTrue : " + isTrue);
                workers[i].ActiveLockArea(!isTrue);
                if (isTrue)
                {
                    workers[i].SetWorkerPanel(npcAgents[i]);
                }
            }
        }

        public void SetAllWorkersActive(bool isTrue)
        {
            for (int i = 0; i < workers.Count; i++)
                workers[i].ActiveLockArea(isTrue);
        }

        public void SetItem(Queue<CreateItemSO> itemQueue)
        {
            foreach(CreateItemSO itemSO in itemQueue)
            {
                CellUI cell = Instantiate(cellPrefab, createItemContent);
                cell.SetCell(itemSO.ResultItem);
            }
        }

        public void ResetItem()
        {
            foreach (Transform child in createItemContent)
                Destroy(child.gameObject);
        }
    }
}
