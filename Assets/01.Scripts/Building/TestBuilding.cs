using UnityEngine;

namespace JMT.Building
{
    public class TestBuilding : BuildingBase
    {
        public override void Build(Vector3 position)
        {
            transform.position = position;
        }

        public override void Work()
        {
            
        }
    }
}