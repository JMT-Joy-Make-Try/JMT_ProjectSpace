using JMT.UISystem;

namespace JMT.Planets.Tile
{
    public class SupplyOxygenInteraction : TileInteraction
    {
        public override void Interaction()
        {
            BuildingUIManager.Instance.OxygenCompo.OpenPanel();
        }
    }
}
