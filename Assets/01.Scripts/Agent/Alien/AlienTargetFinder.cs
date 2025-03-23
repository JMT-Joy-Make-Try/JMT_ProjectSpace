using JMT.Building;
using System;
using UnityEngine;

namespace JMT.Agent.Alien
{
    public class AlienTargetFinder : MonoBehaviour
    {
        public Transform Target { get; private set; }
        
        [field: SerializeField] public float BuildingAttackRange { get; private set; }
        [field: SerializeField] public float AgentAttackRange { get; private set; }
        [field: SerializeField] public LayerMask WhatIsAttackable { get; private set; }
        
        private Collider[] _colliders;
        
        public event Action OnTargetChanged;
        

        private void Awake()
        {
            _colliders = new Collider[10];
        }

        private void Update()
        {
            Target = FindTarget();
        }

        private Transform FindTarget()
        {
            int buildingCount = Physics.OverlapSphereNonAlloc(transform.position, BuildingAttackRange, _colliders, WhatIsAttackable);
            int agentCount = Physics.OverlapSphereNonAlloc(transform.position, AgentAttackRange, _colliders, WhatIsAttackable);
            
            
            if (agentCount > 0)
            {
                for (int i = 0; i < agentCount; i++)
                {
                    if (_colliders[i].TryGetComponent(out Player.Player agent))
                    {
                        OnTargetChanged?.Invoke();
                        return agent.transform;
                    }
                    if (_colliders[i].TryGetComponent(out NPC.NPCAgent npcAgent))
                    {
                        OnTargetChanged?.Invoke();
                        return npcAgent.transform;
                    }
                }
            }
            else if (buildingCount > 0)
            {
                for (int i = 0; i < buildingCount; i++)
                {
                    if (_colliders[i].TryGetComponent(out BuildingBase building))
                    {
                        OnTargetChanged?.Invoke();
                        return building.transform;
                    }
                }
            }
            
            return null;
        }
        
        

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, BuildingAttackRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, AgentAttackRange);
        }
    }
}