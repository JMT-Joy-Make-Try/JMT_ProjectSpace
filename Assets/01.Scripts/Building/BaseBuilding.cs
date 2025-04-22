using JMT.Agent;
using JMT.Agent.NPC;
using JMT.Core.Manager;
using JMT.UISystem;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.Building
{
    public class BaseBuilding : BuildingBase
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _whatIsAgent;
        [SerializeField] private Transform visual, brokenVisual;
        [SerializeField] private int _agentSpawnCount;

        private Collider[] _colliders;

        protected override void Awake()
        {
            base.Awake();
            _colliders = new Collider[10];
        }

        protected override void HandleCompleteEvent()
        {
            base.HandleCompleteEvent();
            FogManager.Instance.OffFogBaseBuilding();
            
            for (int i = 0; i < _agentSpawnCount; i++)
            {
                AgentManager.Instance.SpawnAgent(transform.position + new Vector3(_radius, 0f));
            }
        }


        public override void Work()
        {
            base.Work();
            AgentManager.Instance.SpawnAgent(transform.position + new Vector3(_radius, 0f));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        public void FixStation()
        {
            visual.gameObject.SetActive(true);
            brokenVisual.gameObject.SetActive(false);
            Work();
        }
    }
}