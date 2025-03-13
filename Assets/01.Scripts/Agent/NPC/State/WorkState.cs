using System.Collections;
using UnityEngine;

namespace JMT.Agent.State
{
    public class WorkState : State<NPCState>
    {
        public override void EnterState()
        {
            StartCoroutine(WorkCoroutine());
        }

        private IEnumerator WorkCoroutine()
        {
            var agent = _agent as NPCAgent;
            while (agent.IsWorking)
            {
                yield return new WaitForSeconds(1f);
                agent.CurrentWorkingBuilding.AddNpc(1);
            }
        }
    }
}