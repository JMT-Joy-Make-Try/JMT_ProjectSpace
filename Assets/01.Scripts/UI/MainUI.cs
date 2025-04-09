using UnityEngine;

namespace JMT.UISystem
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public InventoryUI InventoryUI {  get; private set; }
        public GameUI GameUI {  get; private set; }
        public WorkUI WorkUI {  get; private set; }
        public ConstructUI BuildPanelUI { get; private set; }
        public BuildingUI BuildingUI { get; private set; }
        public UpgradeUI UpgradeUI { get; private set; }
        public StationUI StationUI { get; private set; }
        public PopupUI PopupUI { get; private set; }
        public GetUI GetUI { get; private set; }

        private bool isPanelOpen;

        protected override void Awake()
        {
            InventoryUI = GetComponent<InventoryUI>();
            GameUI = GetComponent<GameUI>();
            WorkUI = GetComponent<WorkUI>();
            BuildPanelUI = GetComponent<ConstructUI>();
            UpgradeUI = GetComponent<UpgradeUI>();
            StationUI = GetComponent<StationUI>();
            BuildingUI = GetComponent<BuildingUI>();
            PopupUI = GetComponent<PopupUI>();
            GetUI = GetComponent<GetUI>();
        }
    }
}
