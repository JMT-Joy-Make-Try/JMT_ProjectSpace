using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

namespace JMT.UISystem
{
    [DefaultExecutionOrder(-100)]
    public class MainUI : MonoSingleton<MainUI>
    {
        public NoneUI NoneUI {  get; private set; }
        public ItemUI ItemUI {  get; private set; }
        public BuildingUI BuildingUI {  get; private set; }

        private UIDocument uiDocument;
        private VisualElement root;

        private bool isPanelOpen;

        protected override void Awake()
        {
            uiDocument = GetComponent<UIDocument>();
            NoneUI = GetComponent<NoneUI>();
            ItemUI = GetComponent<ItemUI>();
            BuildingUI = GetComponent<BuildingUI>();
            uiDocument.rootVisualElement.pickingMode = PickingMode.Ignore;
        }

        private void OnEnable()
        {
            root = uiDocument.rootVisualElement;
            NoneUI.SetRoot(root.Q<VisualElement>("NoneUI"));
            ItemUI.SetRoot(root.Q<VisualElement>("ItemUI"));
            BuildingUI.SetRoot(root.Q<VisualElement>("BuildingUI"));
        }
    }
}
