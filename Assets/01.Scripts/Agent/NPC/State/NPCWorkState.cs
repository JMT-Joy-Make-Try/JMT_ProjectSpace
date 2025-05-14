using JMT.Agent.NPC;
using JMT.Building;
using JMT.Building.Component;
using JMT.Core.Tool;
using JMT.Item;
using System.Collections;
using UnityEngine;

namespace JMT.Agent.State
{
    public class NPCWorkState : State<NPCState>
    {
        private NPCAgent npcAgent;
        public override void Initialize(AgentAI<NPCState> agent, string stateName)
        {
            base.Initialize(agent, stateName);
            npcAgent = agent as NPCAgent;
        }

        public override void EnterState()
        {
            base.EnterState();
            npcAgent.MovementCompo.Stop(true);
            npcAgent.transform.rotation = Quaternion.Euler(0, 0, 0);
            npcAgent.transform.localRotation = Quaternion.Euler(0, 0, 0);
            npcAgent.CurrentWorkingBuilding.Work();
            StartCoroutine(Work());
            
        }

        private IEnumerator Work()
        {
            ItemBuilding building = npcAgent.CurrentWorkingBuilding.ConvertTo<ItemBuilding>();
            if (building == null)
            {
                Debug.Log("Building is null");
                yield break;
            }
            
            while (true)
            {
                CreateItemSO item = building.data.GetFirstCreateItem();
                npcAgent.WorkData.SetData(item, item.CreateTime);
                if (item == null || building.data.CreateItemList.Count <= 0)
                {
                    Debug.Log("Building Data is null");
                    yield break;
                }

                int timeMinute = item.CreateTime.GetSecond();
                yield return new WaitForSeconds(timeMinute);
                building.data.RemoveWork();
            }
        }

        public override void UpdateState()
        {
            npcAgent.transform.position = npcAgent.CurrentWorkingBuilding.GetBuildingComponent<BuildingNPC>().WorkPosition.position;
            base.UpdateState();
            npcAgent.transform.rotation = Quaternion.Euler(0, 180, 0);
            npcAgent.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        public override void ExitState()
        {
            npcAgent.MovementCompo.Stop(false);
            base.ExitState();
        }
    }
}