using AYellowpaper.SerializedCollections;
using JMT.Agent;
using System;
using System.Linq;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerTool : AgentCloth<PlayerToolType>, IPlayerComponent
    {
        [SerializeField] private SerializedDictionary<PlayerToolType, ToolSO> _playerToolSOs;
        public ToolSO _curPlayerToolSO;
        public Player Player { get; private set; }
        
        public void Init(Player player)
        {
            Player = player;
            _curPlayerToolSO = _playerToolSOs.First().Value;
        }

        public override void SetCloth(PlayerToolType type)
        {
            base.SetCloth(type);
            Debug.Log(type.ToString());
            _curPlayerToolSO.UnEquip(Player);
            _curPlayerToolSO = _playerToolSOs[type];
            _curPlayerToolSO.Equip(Player);
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
