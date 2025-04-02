using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JMT.UISystem
{
    public class WorkerListUI : MonoBehaviour
    {
        private List<WorkerUI> workers = new();

        private void Awake()
        {
            workers = GetComponentsInChildren<WorkerUI>().ToList();
        }

        public void SetWorkers(int workersCount)
        {
            for(int i = 0; i < workers.Count; i++)
            {
                if (i < workersCount)
                {
                    workers[i].gameObject.SetActive(true);
                    Debug.Log("로동자들 스탯 로드해줘야함.");
                    // workers[i].SetStat();
                }
                else
                    workers[i].gameObject.SetActive(false);
            }
        }
    }
}
