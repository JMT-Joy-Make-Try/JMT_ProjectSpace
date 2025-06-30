using JMT.Building;
using JMT.Item;
using JMT.DataSystem;
using JMT.UISystem;
using System.Linq;

namespace JMT.Planets.Tile
{
    public class VillageInteraction : TileInteraction
    {
        private VillageBuilding _villageBuilding;
        private VillageSO villageSO;
        
        protected override void Awake()
        {
            base.Awake();
            _villageBuilding = GetComponentInChildren<VillageBuilding>();
        }

        private void Start()
        {
            villageSO = RandomDataSystem.Instance.GetRandomVillageSO();
        }

        public override void Interaction()
        {
            base.Interaction();
            GameUIManager.Instance.TradeCompo.OpenPanel(villageSO);
            /*ItemSO item = _villageBuilding.NeedItems.First().Key;
            GameUIManager.Instance.InventoryCompo.RemoveItem(item, 1);
            _villageBuilding.GiveItem(item, 1);*/
        }
    }
}