using JMT.Building;
using JMT.Planets.Tile;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Planet.Tile
{
    public class FactoryInteraction : TileInteraction
    {
        [SerializeField] private CreateItemSO createItemSO;
        protected override void Awake()
        {
            base.Awake();
            //GameUIManager.Instance.InteractCompo.OnHoldEvent += HandleHoldEvent;
        }

        public override void Interaction()
        {
            base.Interaction();
            BuildingUIManager.Instance.FactoryCompo.OpenPanel();
        }

        private void HandleHoldEvent(bool obj)
        {
            //Debug.Log(obj);
            //(planetTile.CurrentBuilding as FactoryBuilding)?.SetHold(obj);
        }
    }
}