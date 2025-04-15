using JMT.Agent;
using JMT.Agent.NPC;
using JMT.Core.Manager;
using JMT.Resource;
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

        private Collider[] _colliders;

        protected override void Awake()
        {
            base.Awake();
            _colliders = new Collider[10];
        }

        private void Start()
        {
            FogManager.Instance.OffFogBaseBuilding();
        }


        public override void Work()
        {
            base.Work();
            // if (gameObject.activeSelf)
            // {
            //     StartCoroutine(WorkCoroutine());
            // }
            AgentManager.Instance.SpawnAgent(transform.position + new Vector3(_radius, 0f));
        }

        private IEnumerator WorkCoroutine()
        {
            while (_isWorking)
            {
                int cnt = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _whatIsAgent);
                for (int i = 0; i < cnt; i++)
                {
                    if (_colliders[i].TryGetComponent(out NPCAgent agent))
                    {
                        ResourceManager.Instance.AddNpc(-1);
                        agent.OxygenCompo.AddOxygen(1);
                    }
                }

                yield return new WaitForSeconds(10f);
            }
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