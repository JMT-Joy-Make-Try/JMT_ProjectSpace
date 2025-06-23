using JMT.Building;
using JMT.NightSummary;
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
        }

        private void UpgradeRocketLauncher()
        {
            NightSummaryManager.Instance.RocketStatusModule.UpgradeRocketCompletion(25);
        }
    }
}