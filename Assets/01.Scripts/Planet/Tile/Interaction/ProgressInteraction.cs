using JMT.Building;
using JMT.Planets.Tile;
using UnityEngine;

namespace JMT
{
    public class ProgressInteraction : TileInteraction
    {
        public override void Interaction()
        {
            BuildingBase building = GetComponentInChildren<BuildingBase>();
            Debug.Log(building);

            if (!building.IsBuilding)
            {
                Debug.LogError("Dasddadjkadjlkdjl");
                return;
            }
            building.OnCompleteEvent?.Invoke();

            TileManager.Instance.CurrentTile.RemoveInteraction();
            if (building is BaseBuilding)
            {
                TileManager.Instance.CurrentTile.AddInteraction<StationInteraction>();
                return;
            }
            TileManager.Instance.CurrentTile.AddInteraction<BuildingInteraction>();
        }
    }
}
