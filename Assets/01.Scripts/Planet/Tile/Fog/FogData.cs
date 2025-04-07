using System.Collections.Generic;
using UnityEngine;

namespace JMT.Planets.Tile
{
	[CreateAssetMenu(fileName = "FogData", menuName = "SO/FogData")]
    public class FogData : ScriptableObject
    {
        public List<List<bool>> fogData = new List<List<bool>>();
    }
}