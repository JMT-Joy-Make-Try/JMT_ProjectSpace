using JMT.Core.Manager;
using JMT.UISystem;
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
            Debug.Log(list.FogTier -1);
            if (!FogManager.Instance.IsAllFogOff(list.FogTier - 1))
            {
                UIManager.Instance.PopupUI.SetPopupText("전 단계의 안개가 모두 해금되지 않았습니다.");
                return;
            }
            base.Interaction();
            list.Tiles.ForEach(tile => tile.Fog.SetFog(false));
            Destroy(_zeolitePrefab);
        }
    }
}