using JMT.Building.Component;
using JMT.NightSummary;

namespace JMT.Building
{
    public class RocketLauncherBuilding : BuildingBase
    {
        protected override void AddEvents()
        {
            base.AddEvents();
            GetBuildingComponent<BuildingLevel>().OnLevelChanged += HandleLevelChanged;
            GetBuildingComponent<BuildingLevel>().OnUpgradeComplete += HandleUpgradeComplete;
        }
        
        protected override void RemoveEvents()
        {
            base.RemoveEvents();
            GetBuildingComponent<BuildingLevel>().OnLevelChanged -= HandleLevelChanged;
            GetBuildingComponent<BuildingLevel>().OnUpgradeComplete -= HandleUpgradeComplete;
        }

        private void HandleUpgradeComplete()
        {
            // 연출
        }

        private void HandleLevelChanged(int level)
        {
            UpgradeRocketLauncher();
        }

        private void UpgradeRocketLauncher()
        {
            NightSummaryManager.Instance.RocketStatusModule.UpgradeRocketCompletion(25);
        }
    }
}