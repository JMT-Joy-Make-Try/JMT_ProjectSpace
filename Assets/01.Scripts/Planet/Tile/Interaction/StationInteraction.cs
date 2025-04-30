using JMT.UISystem;

namespace JMT.Planets.Tile
{
    public class StationInteraction : TileInteraction
    {
        public override void Interaction()
        {
            BuildingUIManager.Instance.StationCompo.OpenUI();
        }
    }
}
