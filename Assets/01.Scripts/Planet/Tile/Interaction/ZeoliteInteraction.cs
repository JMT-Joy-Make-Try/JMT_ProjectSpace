using JMT.Core.Manager;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class ZeoliteInteraction : TileInteraction
    {
        private GameObject _zeolitePrefab;

        protected override void Awake()
        {
            base.Awake();
            _zeolitePrefab = transform.GetChild(0).gameObject;
        }

        public override void Interaction()
        {
            TileList list = transform.parent.parent.GetComponent<TileList>();
            // if (!FogManager.Instance.IsAllFogOff(list.FogTier - 1))
            // {
            //     Debug.Log("Fog is not off");
            //     return;
            // }
            base.Interaction();
            list.Tiles.ForEach(tile => tile.Fog.SetFog(false));
            Destroy(_zeolitePrefab);
        }
    }
}