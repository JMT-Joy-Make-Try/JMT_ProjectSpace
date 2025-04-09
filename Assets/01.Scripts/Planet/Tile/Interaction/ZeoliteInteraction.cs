using UnityEngine;

namespace JMT.Planets.Tile
{
    public class ZeoliteInteraction : TileInteraction
    {
        public override void Interaction()
        {
            base.Interaction();
            TileList list = transform.parent.parent.GetComponent<TileList>();
            list.Tiles.ForEach(tile => tile.Fog.SetFog(false));
        }
    }
}