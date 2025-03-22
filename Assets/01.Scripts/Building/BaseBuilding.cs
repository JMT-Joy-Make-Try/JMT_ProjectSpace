using JMT.Agent;
using JMT.Agent.NPC;
using JMT.Resource;
using System.Collections;
using UnityEngine;

namespace JMT.Building
{
    public class BaseBuilding : BuildingBase
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _whatIsAgent;

        private Collider[] _colliders;

        protected override void Awake()
        {
            base.Awake();
            _colliders = new Collider[10];
        }

        public override void Build(Vector3 position, Transform parent)
        {
            transform.position = position;
            transform.SetParent(parent);
            AgentManager.Instance.SpawnAgent(position + new Vector3(_radius, 0));
            Work();
        }

        public override void Work()
        {
            if (_isWorking)
            {
                return;
            }

            _isWorking = true;
            StartCoroutine(WorkCoroutine());
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
                        ResourceManager.Instance.AddOxygen(-1);
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
    }
}