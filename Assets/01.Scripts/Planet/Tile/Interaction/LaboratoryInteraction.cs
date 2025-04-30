using JMT.UISystem;

namespace JMT.Planets.Tile
{
    public class LaboratoryInteraction : TileInteraction
    {
        public override void Interaction()
        {
            BuildingUIManager.Instance.LaboratoryCompo.OpenUI();
        }
    }
}
