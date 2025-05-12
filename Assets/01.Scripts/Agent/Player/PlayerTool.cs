using AYellowpaper.SerializedCollections;
using JMT.Agent;
using System;
using System.Linq;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerTool : AgentCloth<PlayerToolType>
    {
        [SerializeField] private SerializedDictionary<PlayerToolType, ToolSO> _playerToolSOs;
        public ToolSO _curPlayerToolSO;
        private Player _player;
        
        public void Init(Player player)
        {
            _player = player;
            _curPlayerToolSO = _playerToolSOs.First().Value;
        }

        public override void SetCloth(PlayerToolType type)
        {
            base.SetCloth(type);
            Debug.Log(type.ToString());
            _curPlayerToolSO.UnEquip(_player);
            _curPlayerToolSO = _playerToolSOs[type];
            _curPlayerToolSO.Equip(_player);
        }
    }
    
    public enum PlayerToolType
    {
        Vacuum, // 먼지채집기
        Scanner, // 유기물채집기
        FuelDropper, // 액체연료채집기
        FilterMask, // 필터마스크
        Hammer, // 망치
    }
}
