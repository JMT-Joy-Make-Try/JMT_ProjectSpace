using System.Collections.Generic;
using JMT.Planets.Tile;
using JMT.Building;
using JMT.Planets;
using System;

namespace JMT
{
    public class ProgressInteraction : TileInteraction
    {
        private static readonly Dictionary<Type, Type> _interactionLookup = new()
        {
            { typeof(BaseBuilding), typeof(StationInteraction) },
            { typeof(LaboratoryBuilding), typeof(LaboratoryInteraction) },
            { typeof(OxygenBuilding), typeof(SupplyOxygenInteraction) },
            { typeof(HospitalBuilding), typeof(HospitalInteraction) }
        };
        public override void Interaction()
        {
            base.Interaction();
            BuildingBase building = GetComponentInChildren<BuildingBase>();

            if (!building.IsBuilding) return;
            building.OnCompleteEvent?.Invoke();
            
            var tile = TileManager.Instance.CurrentTile;

            tile.RemoveInteraction();
            var buildingType = building.GetType();
            if (_interactionLookup.TryGetValue(buildingType, out var interactionType))
            {
                var method = typeof(PlanetTile).GetMethod("AddInteraction", Type.EmptyTypes);
                var generic = method.MakeGenericMethod(interactionType);
                generic.Invoke(tile, null);
                return;
            }

            tile.AddInteraction<BuildingInteraction>();
        }
    }
}
