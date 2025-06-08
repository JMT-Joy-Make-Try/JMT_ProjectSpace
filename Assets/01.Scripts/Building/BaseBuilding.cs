using JMT.Agent;
using JMT.Building.Component;
using JMT.Core;
using JMT.Core.Manager;
using JMT.Core.Tool;
using JMT.Item;
using JMT.PlayerCharacter;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Building
{
    public class BaseBuilding : BuildingBase, IItemReceivable
    {
        [SerializeField] private Transform visual, brokenVisual;

        private Vector3 _playerPos;
        private Player _player;

        protected override void HandleCompleteEvent()
        {
            base.HandleCompleteEvent();
            FogManager.Instance.OffFogBaseBuilding();
            FixStation();
            GetBuildingComponent<BuildingAnimator>().SetAnimation(true);
        }


        public void FixStation()
        {
            visual.gameObject.SetActive(true);
            brokenVisual.gameObject.SetActive(false);
        }

        public bool ReceiveItem(ItemSO item, int amount)
        {
            if (!IsBuildingComplete) return false;
            BuildingUIManager.Instance.StorageCompo.AddItem(item, amount);
            return true;
        }
    }
}