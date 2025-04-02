using JMT.Building;
using System;
using UnityEngine;

namespace JMT.Agent.Alien
{
    public class AlienTargetFinder : MonoBehaviour
    {
        public Transform Target { get; set; }

        [field: SerializeField] public float BuildingAttackRange { get; private set; }
        [field: SerializeField] public float AgentAttackRange { get; private set; }
        [field: SerializeField] public LayerMask WhatIsAttackable { get; private set; }

        private Collider[] _colliders;

        public event Action OnTargetChanged;

        private void Awake()
        {
            _colliders = new Collider[20];
        }

        private void Update()
        {
            Transform newTarget = FindTarget();
            if (Target != newTarget)
            {
                Target = newTarget;
                OnTargetChanged?.Invoke();
            }
        }

        private Transform FindTarget()
        {
            Transform npcTarget = null;
            Transform buildingTarget = null;

            int buildingCount = Physics.OverlapSphereNonAlloc(transform.position, BuildingAttackRange, _colliders, WhatIsAttackable);
            for (int i = 0; i < buildingCount; i++)
            {
                if (_colliders[i].transform.parent != null)
                {
                    if (_colliders[i].transform.parent.TryGetComponent(out BuildingBase building) && buildingTarget == null)
                    {
                        buildingTarget = building.transform;
                    }
                }
            }

            int agentCount = Physics.OverlapSphereNonAlloc(transform.position, AgentAttackRange, _colliders, WhatIsAttackable);
            for (int i = 0; i < agentCount; i++)
            {
                if (_colliders[i].TryGetComponent(out Player.Player player))
                {
                    return player.transform;
                }
                if (_colliders[i].TryGetComponent(out NPC.NPCAgent npcAgent) && npcTarget == null)
                {
                    npcTarget = npcAgent.transform;
                }
            }

            return npcTarget ?? buildingTarget;
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
