using JMT.Agent.NPC;
using JMT.Item;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace JMT.UISystem.Building
{
    public class ManageView : MonoBehaviour
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

        public void AddItem(ItemSO itemSO, int count)
        {
            CellUI cell = Instantiate(cellPrefab, createItemContent);
            cell.SetCell(itemSO, $"X {count}");
            //itemQueue.Enqueue(cell);
            Debug.Log("제작할 아이템을 대기열에 추가했습니다.");
        }
        
        public void RemoveItem()
        {
            // if (itemQueue.Count > 0)
            // {
            //     Destroy(itemQueue.Dequeue().gameObject);
            //     Debug.Log("제작할 아이템을 대기열에서 제거합니다.");
            // }
            //else Debug.Log("대기열에 아이템이 존재하지 않습니다.");
        }

        
    }
}
