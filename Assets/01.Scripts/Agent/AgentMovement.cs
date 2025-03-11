using System;
using JMT.Core.Tool;
using UnityEngine;
using UnityEngine.AI;

namespace JMT.Agent
{
    public class AgentMovement : MonoBehaviour
    {
        [field:SerializeField] public NavMeshAgent NavMeshAgentCompo { get; private set; }

        private void Awake()
        {
            if (NavMeshAgentCompo == null)
                NavMeshAgentCompo = gameObject.GetComponentOrAdd<NavMeshAgent>();
        }
        
        public void Move(Vector3 destination, float speed)
        {
            NavMeshAgentCompo.speed = speed;
            NavMeshAgentCompo.SetDestination(destination);
        }
        
        public void Stop(bool isStop)
        {
            NavMeshAgentCompo.isStopped = isStop;
        }
    }
}