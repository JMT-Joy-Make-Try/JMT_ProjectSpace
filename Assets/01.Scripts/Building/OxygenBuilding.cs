using JMT.Core;
using System;
using UnityEngine;

namespace JMT.Building
{
    public class OxygenBuilding : ItemBuilding
    {
        private int _purificationContainerAmount = 0;
        private int _oxygenContainerAmount = 0;
        public override void Build(Vector3 position, Transform parent)
        {
        }

        public override void Work()
        {
            base.Work();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out IOxygen oxygen))
            {
                AddOxygen(oxygen);
            }
        }

        public void AddOxygen(IOxygen oxygen)
        {
            oxygen.AddOxygen(1);
            _purificationContainerAmount -= 1;
        }
    }
}