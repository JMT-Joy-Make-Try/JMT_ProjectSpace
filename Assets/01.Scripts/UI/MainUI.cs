using UnityEngine;

namespace JMT.UISystem
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public InventoryUI InventoryUI {  get; private set; }
        public WorkUI WorkUI {  get; private set; }
        public ConstructUI BuildPanelUI { get; private set; }
        public BuildingUI BuildingUI { get; private set; }
        public UpgradeUI UpgradeUI { get; private set; }
        public CreateUI CreateUI { get; private set; }
        public ManageUI ManageUI { get; private set; }

        private bool isPanelOpen;

        protected override void Awake()
        {
            InventoryUI = GetComponent<InventoryUI>();
            WorkUI = GetComponent<WorkUI>();
            BuildPanelUI = GetComponent<ConstructUI>();
            UpgradeUI = GetComponent<UpgradeUI>();
            CreateUI = GetComponent<CreateUI>();
            ManageUI = GetComponent<ManageUI>();
            BuildingUI = GetComponent<BuildingUI>();
        }
    }
}
