using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.Building
{
    public class TestBuilding : BuildingBase
    {
        public override void Build(Vector3 position, Transform parent)
        {
            transform.position = position;
        }
    }
}