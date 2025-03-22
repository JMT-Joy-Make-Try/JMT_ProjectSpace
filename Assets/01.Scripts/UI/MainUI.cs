using UnityEngine;

namespace JMT.UISystem
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public NoneUI NoneUI {  get; private set; }
        public ItemUI ItemUI {  get; private set; }
        public BuildingUI BuildingUI {  get; private set; }
        public InventoryUI InventoryUI {  get; private set; }
        public WorkUI WorkUI {  get; private set; }
        public BuildPanelUI BuildPanelUI { get; private set; }
        //public NoTouchUI NoTouchUI { get; private set; }
        public UpgradeUI UpgradeUI { get; private set; }
        public CreateUI CreateUI { get; private set; }
        public ManageUI ManageUI { get; private set; }

        private bool isPanelOpen;

        protected override void Awake()
        {
            NoneUI = GetComponent<NoneUI>();
            ItemUI = GetComponent<ItemUI>();
            BuildingUI = GetComponent<BuildingUI>();
            InventoryUI = GetComponent<InventoryUI>();
            WorkUI = GetComponent<WorkUI>();
            BuildPanelUI = GetComponent<BuildPanelUI>();
            UpgradeUI = GetComponent<UpgradeUI>();
            CreateUI = GetComponent<CreateUI>();
            ManageUI = GetComponent<ManageUI>();
            //NoTouchUI = GetComponent<NoTouchUI>();
        }
    }
}
