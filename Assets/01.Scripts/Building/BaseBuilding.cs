using JMT.Core.Manager;
using UnityEngine;

namespace JMT.Building
{
    public class BaseBuilding : BuildingBase
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _whatIsAgent;
        [SerializeField] private Transform visual, brokenVisual;
        [SerializeField] private int _agentSpawnCount;


        protected override void HandleCompleteEvent()
        {
            base.HandleCompleteEvent();
            FogManager.Instance.OffFogBaseBuilding();
            FixStation();
        }


        public void FixStation()
        {
            visual.gameObject.SetActive(true);
            brokenVisual.gameObject.SetActive(false);
        }
    }
}