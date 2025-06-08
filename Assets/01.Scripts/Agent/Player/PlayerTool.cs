using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Item;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace JMT.PlayerCharacter
{
    public class PlayerTool : AgentCloth<PlayerToolType>, IPlayerComponent
    {
        public Player Player { get; private set; }
        [SerializeField] private SerializedDictionary<PlayerToolType, ToolSO> _playerToolSOs;
        public ToolSO CurPlayerToolSO { get; private set; }
        
        public void Init(IPlayer player)
        {
            Player = player as Player;
            foreach (var tool in _playerToolSOs)
            {
                _playerToolSOs[tool.Key] = Instantiate(tool.Value);
                _playerToolSOs[tool.Key].Init(player);
            }
            CurPlayerToolSO = _playerToolSOs.First().Value;
        }

        public override void SetCloth(PlayerToolType type)
        {
            base.SetCloth(type);
            Debug.Log(type.ToString());
            CurPlayerToolSO.UnEquip();
            CurPlayerToolSO = _playerToolSOs[type];
            CurPlayerToolSO.Equip();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetCloth(PlayerToolType.Farmer);
            }
        }
    }
    
    public enum PlayerToolType
    {
        Vacuum, // 먼지채집기
        Scanner, // 유기물채집기
        FuelDropper, // 액체연료채집기
        FilterMask, // 필터마스크
        Hammer, // 망치
        Farmer, // 농기구
    }
}
