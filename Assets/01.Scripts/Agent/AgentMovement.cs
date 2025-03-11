using System;
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
            {
                NavMeshAgentCompo = GetComponent<NavMeshAgent>();
                if (NavMeshAgentCompo == null)
                {
                    NavMeshAgentCompo = GetComponentInParent<NavMeshAgent>();
                }
            }
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