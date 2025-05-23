using System;
using JMT.Core.Tool;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace JMT.Agent
{
    public class AgentMovement : MonoBehaviour
    {
        [field:SerializeField] public NavMeshAgent NavMeshAgentCompo { get; private set; }
        [SerializeField] protected float _remainDistance = 0.5f;
        
        public bool IsMoving => NavMeshAgentCompo.velocity.magnitude > 0.1f;

        private void Awake()
        {
            if (NavMeshAgentCompo == null)
                NavMeshAgentCompo = gameObject.GetComponentOrAdd<NavMeshAgent>();
        }
        
        private void Start()
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

        
        public void Move(Vector3 destination, float speed, Action onComplete = null)
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
        
        private IEnumerator WaitForArrival(Action onComplete)
        {
            while (NavMeshAgentCompo.pathPending || NavMeshAgentCompo.remainingDistance > _remainDistance)
            {
                yield return null;
            }
    
            onComplete?.Invoke();
        }

        
        public void Stop(bool isStop)
        {
            NavMeshAgentCompo.isStopped = isStop;
        }
    }
}