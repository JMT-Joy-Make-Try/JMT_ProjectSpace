using JMT.UISystem;

namespace JMT.Planets.Tile
{
    public class StationInteraction : TileInteraction
    {
        public override void Interaction()
        {
            UIManager.Instance.StationUI.OpenUI();
        }
    }
}
