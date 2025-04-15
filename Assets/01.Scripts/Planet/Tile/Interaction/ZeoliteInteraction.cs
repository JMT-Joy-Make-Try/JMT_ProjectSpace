using UnityEngine;

namespace JMT.Planets.Tile
{
    public class ZeoliteInteraction : TileInteraction
    {
        private GameObject zeolitePrefab;

        protected override void Awake()
        {
            base.Awake();
            zeolitePrefab = transform.GetChild(0).gameObject;
        }

        public override void Interaction()
        {
            base.Interaction();
            TileList list = transform.parent.parent.GetComponent<TileList>();
            list.Tiles.ForEach(tile => tile.Fog.SetFog(false));
            Destroy(zeolitePrefab);
        }
    }
}