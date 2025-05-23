using JMT.Building;
using JMT.Item;
using JMT.UISystem;
using System.Linq;

namespace JMT.Planets.Tile
{
    public class VillageInteraction : TileInteraction
    {
        private VillageBuilding _villageBuilding;
        
        protected override void Awake()
        {
            base.Awake();
            _villageBuilding = GetComponentInChildren<VillageBuilding>();
        }
        public override void Interaction()
        {
            base.Interaction();
            ItemSO item = _villageBuilding.NeedItems.First().Key;
            GameUIManager.Instance.InventoryCompo.RemoveItem(item, 1);
            _villageBuilding.GiveItem(item, 1);
        }
    }
}