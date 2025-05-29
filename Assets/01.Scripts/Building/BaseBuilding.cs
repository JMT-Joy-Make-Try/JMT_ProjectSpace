using JMT.Agent;
using JMT.Building.Component;
using JMT.Core.Manager;
using JMT.Core.Tool;
using JMT.PlayerCharacter;
using JMT.UISystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building
{
    public class BaseBuilding : BuildingBase
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

        private void Update()
        {
            _player = AgentManager.Instance.Player;
            _playerPos = _player.transform.position;
            if (_playerPos.IsNear(transform.position, 10f))
            {
                if (_player.InventoryCompo.PlayerInventoryData.count > 0 && _player.InventoryCompo.PlayerInventoryData.item != null)
                {
                    var item = _player.InventoryCompo.RemoveItem();
                    if (item == null) return;
                    GameUIManager.Instance.InventoryCompo.AddItem(item, 1);
                }
            }
        }
    }
}