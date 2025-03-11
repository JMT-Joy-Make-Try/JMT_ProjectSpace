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
        
        void Start()
        {
            if (!NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                Debug.LogError($"[{gameObject.name}] NavMesh 위에 있지 않음!");
            }
            else
            {
                Debug.Log($"[{gameObject.name}] NavMesh 위에 있음!");
            }
            
            Stop(false);
        }

        
        public void Move(Vector3 destination, float speed)
        {
            NavMeshAgentCompo.speed = speed;
            NavMeshAgentCompo.SetDestination(destination);
            Debug.Log(NavMeshAgentCompo.destination);
            Debug.Log(NavMeshAgentCompo.speed);
        }
        
        public void Stop(bool isStop)
        {
            NavMeshAgentCompo.isStopped = isStop;
        }
    }
}