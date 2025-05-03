using JMT.UISystem.DayTime;
using JMT.UISystem.GameSpeed;
using JMT.UISystem.Interact;
using JMT.UISystem.Inventory;
using JMT.UISystem.ItemGet;
using JMT.UISystem.Popup;
using JMT.UISystem.Resource;
using UnityEngine;
using UnityEngine.Windows;

namespace JMT.UISystem
{
    public class GameUIManager : MonoSingleton<GameUIManager>
    {
        [SerializeField] private PlayerInputSO inputSO;

        [SerializeField] private TimeController timeCompo;
        [SerializeField] private GameSpeedController speedCompo;
        [SerializeField] private ResourceController resourceCompo;
        [SerializeField] private InventoryController inventoryCompo;
        [SerializeField] private ItemGetController itemGetCompo;
        [SerializeField] private PopupController popupCompo;
        [SerializeField] private ConstructController constructCompo;
        [SerializeField] private InteractController interactCompo;
        [SerializeField] private GameUIController gameUICompo;
        public TimeController TimeCompo => timeCompo;
        public GameSpeedController SpeedCompo => speedCompo;
        public ResourceController ResourceCompo => resourceCompo;
        public InventoryController InventoryCompo => inventoryCompo;
        public ItemGetController ItemGetCompo => itemGetCompo;
        public PopupController PopupCompo => popupCompo;
        public ConstructController ConstructCompo => constructCompo;
        public InteractController InteractCompo => interactCompo;
        public GameUIController GameUICompo => gameUICompo;

        public void PlayerControlActive(bool isActive)
        {
            if (isActive)
                inputSO.ControlEnable(true);
            else
                inputSO.ControlEnable(false);
        }
    }
}
