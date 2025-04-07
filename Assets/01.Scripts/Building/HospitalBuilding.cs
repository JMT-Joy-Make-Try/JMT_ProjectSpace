using System.Collections;
using UnityEngine;

namespace JMT.Building
{
    public class HospitalBuilding : BuildingBase
    {
        [field: SerializeField] public float HealingTime = 5f;
        
        public override void Build(Vector3 position, Transform parent)
        {
            
        }
    }
}