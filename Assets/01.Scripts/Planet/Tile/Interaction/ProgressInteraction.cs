using JMT.Agent;
using System.Collections.Generic;
using JMT.Planets.Tile;
using JMT.Building;
using JMT.Building.Component;
using JMT.Planets;
using System;
using UnityEngine;

namespace JMT.Planet.Tile
{
    public class ProgressInteraction : TileInteraction
    {
        private static readonly Dictionary<Type, Type> _interactionLookup = new()
        {
            { typeof(BaseBuilding), typeof(StationInteraction) },
            { typeof(LaboratoryBuilding), typeof(LaboratoryInteraction) },
            { typeof(OxygenBuilding), typeof(SupplyOxygenInteraction) },
            { typeof(HospitalBuilding), typeof(HospitalInteraction) },
            { typeof(RocketBuilding), typeof(RocketLauncherInteraction) }
        };
        
        public override void Interaction()
        {
            base.Interaction();
            BuildingBase building = GetComponentInChildren<BuildingBase>();
            var builder = building.GetBuildingComponent<BuildingBuilder>();
            if (!builder.IsBuilding) return;
            builder.CompleteEventInvoker();
            
            var tile = planetTile;

            tile.RemoveInteraction();
            var buildingType = building.GetType();
            if (_interactionLookup.TryGetValue(buildingType, out var interactionType))
            {
                if (interactionType == typeof(RocketLauncherInteraction))
                {
                    RocketLauncherInteraction(tile);
                    return;
                }
                var method = typeof(PlanetTile).GetMethod("AddInteraction", Type.EmptyTypes);
                var generic = method.MakeGenericMethod(interactionType);
                generic.Invoke(tile, null);
                return;
            }

            tile.AddInteraction<BuildingInteraction>();
        }
        
        private void RocketLauncherInteraction(PlanetTile tile)
        {
            var playerLookDir = AgentManager.Instance.Player.VisualTrm.forward;
            var tiles = TileManager.Instance.Get2By2TilesInAnyDirection(tile, playerLookDir);
            foreach (var t in tiles)
            {
                Debug.LogError($"Adding interaction RocketLauncherInteraction to tile at position {t.Position}");
                t.AddInteraction<RocketLauncherInteraction>();
            }
        }
    }
}
