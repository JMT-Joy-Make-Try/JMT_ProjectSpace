using JMT.Building;
using JMT.NightSummary;
using JMT.UISystem;
using System;
using UnityEngine;


namespace JMT.Planets.Tile
{
    public class RocketLauncherInteraction : TileInteraction
    {
        private RocketLauncherBuilding _rocketLauncherBuilding;
        
        public override void Interaction()
        {
            base.Interaction();
            // Open the rocket launcher UI
            BuildingUIManager.Instance.RocketCompo.OpenPanel();
        }
    }
}