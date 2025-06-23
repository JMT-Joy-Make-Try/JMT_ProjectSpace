using JMT.Agent.NPC;
using JMT.Building;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace JMT.Agent
{
    public class NPCMovement : AgentMovement, INPCComponent
    {
        public BuildingBase Building { get; private set; }
        public void Move(Vector3 destination, float speed, Action<BuildingBase> onComplete = null)
        {

            if (!NavMesh.SamplePosition(destination, out NavMeshHit hit, 1000.0f, NavMesh.AllAreas))
            {
                Debug.LogError($"잘못된 목적지: {destination}");
                return;
            }


            NavMeshAgentCompo.speed = speed;
            NavMeshAgentCompo.SetDestination(hit.position);
            if (gameObject.activeSelf && onComplete != null)
                StartCoroutine(WaitForArrival(onComplete));

            if (NavMeshAgentCompo.pathStatus != NavMeshPathStatus.PathComplete)
            {
                Debug.LogError($"이동할 수 없는 경로! {destination}");
            }
        }
        
        private IEnumerator WaitForArrival(Action<BuildingBase> onComplete)
        {
            while (NavMeshAgentCompo.pathPending || NavMeshAgentCompo.remainingDistance > _remainDistance)
            {
                yield return null;
            }
    
            onComplete?.Invoke(Building);
        }
        
        public void SetBuilding(BuildingBase building)
        {
            Building = building;
        }

        public NPCAgent Agent { get; private set; }
        public void Initialize(NPCAgent agent)
        {
            Agent = agent;
        }
    }
}