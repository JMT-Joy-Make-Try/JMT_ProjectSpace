using System.Collections.Generic;
using UnityEngine;

namespace JMT.Planets.Tile
{
    [CreateAssetMenu(fileName = "TilesSO", menuName = "TilesSO")]
    public class TilesSO : ScriptableObject
    {
        public List<TileData> tiles;
        
        public int AllTileCount
        {
            get
            {
                var count = 0;
                foreach (var tileData in tiles)
                {
                    count += tileData.Count;
                }

                return count;
            }
        }
    }
    
    [System.Serializable]
    public class TileData
    {
        public TileType TileType;
        [ColorUsage(true, true)]
        public Color Color;
        public int Count;
    }
}