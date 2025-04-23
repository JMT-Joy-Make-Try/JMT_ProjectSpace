using JMT.Agent.NPC;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.UISystem.Building
{
    public class ManageView : MonoBehaviour
    {
        [SerializeField] private Transform workerManageContent;
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
            }
        }

        public void SetAllWorkersActive(bool isTrue)
        {
            for (int i = 0; i < workers.Count; i++)
                workers[i].ActiveLockArea(isTrue);
        }
    }
}
