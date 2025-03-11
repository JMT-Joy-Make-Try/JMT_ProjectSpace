using UnityEngine;

namespace JMT.Building
{
    public class TestBuilding : BuildingBase
    {
        public override void Build(Vector3 position)
        {
            transform.position = position;
        }

        protected override void Work()
        {
            if (_currentNpcCount < NpcCount)
            {
                Debug.Log("Not enough NPC");
            }
            else
            {
                Debug.Log("Work");
            }
        }
    }
}