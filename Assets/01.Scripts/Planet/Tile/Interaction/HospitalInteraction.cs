using JMT.Planets.Tile;
using JMT.UISystem;

namespace JMT.Planets
{
    public class HospitalInteraction : TileInteraction
    {
        public override void Interaction()
        {
            BuildingUIManager.Instance.HospitalCompo.OpenPanel();
        }
    }
}
