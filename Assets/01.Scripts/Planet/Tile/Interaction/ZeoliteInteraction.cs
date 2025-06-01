using JMT.Core.Manager;
using JMT.UISystem;
using System;
using System.Collections;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class ZeoliteInteraction : TileInteraction
    {
        public event Action OnInteraction;
        
        private TileList _list;
        
        protected override void Awake()
        {
            base.Awake();
            _list = transform.parent.parent.GetComponent<TileList>();
        }

        public override void Interaction()
        {
            if (!FogManager.Instance.IsAllFogOff(_list.FogTier - 1))
            {
                GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("전 단계의 안개가 모두 해금되지 않았습니다.");
                return;
            }
            base.Interaction();
            //StartCoroutine(FogOff());
            OnInteraction?.Invoke();
            _list.Tiles.ForEach(tile => tile.Fog.SetFog(false));
            _list.SetLineRenderer(false);
            TileManager.Instance.CurrentTile.RemoveInteraction();
            TileManager.Instance.CurrentTile.AddInteraction<NoneInteraction>();
        }

        private IEnumerator FogOff()
        {
            
            yield return new WaitForSeconds(1f);
            
        }
    }
}