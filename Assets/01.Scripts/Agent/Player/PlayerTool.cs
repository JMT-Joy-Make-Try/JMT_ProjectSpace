using AYellowpaper.SerializedCollections;
using JMT.Agent;
using System;
using UnityEngine;

namespace JMT.Player
{
    public class PlayerTool : AgentCloth<PlayerToolType>
    {
        [SerializeField] private SerializedDictionary<PlayerToolType, ToolSO> _playerToolSOs;
        private ToolSO _curPlayerToolSO;
        private Player _player;
        
        public void Init(Player player)
        {
            _player = player;
        }

        public override void SetCloth(PlayerToolType type)
        {
            base.SetCloth(type);
            _curPlayerToolSO.UnEquip(_player);
            _curPlayerToolSO = _playerToolSOs[type];
            _curPlayerToolSO.Equip(_player);
        }
    }
    
    public enum PlayerToolType
    {
        DustCollector, // 먼지채집기
        OrganicMatterCollector, // 유기물채집기
        LiquidFuelCollector, // 액체연료채집기
        FilterMask, // 필터마스크
        ProtectiveClothing, // 방호복
    }
}
