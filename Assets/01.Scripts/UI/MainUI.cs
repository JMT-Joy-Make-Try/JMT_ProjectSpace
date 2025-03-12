using UnityEngine;

namespace JMT.UISystem
{
    [DefaultExecutionOrder(-100)]
    public class UIManager : MonoSingleton<UIManager>
    {
        public NoneUI NoneUI {  get; private set; }
        public ItemUI ItemUI {  get; private set; }
        public BuildingUI BuildingUI {  get; private set; }
        public InventoryUI InventoryUI {  get; private set; }
        public BuildPanelUI BuildPanelUI { get; private set; }

        private bool isPanelOpen;

        protected override void Awake()
        {
            NoneUI = GetComponent<NoneUI>();
            ItemUI = GetComponent<ItemUI>();
            BuildingUI = GetComponent<BuildingUI>();
            InventoryUI = GetComponent<InventoryUI>();
            BuildPanelUI = GetComponent<BuildPanelUI>();
        }
    }
}
