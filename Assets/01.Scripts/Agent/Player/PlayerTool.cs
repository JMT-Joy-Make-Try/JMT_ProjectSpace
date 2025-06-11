using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace JMT.PlayerCharacter
{
    public class PlayerTool : AgentCloth<PlayerToolType>, IPlayerComponent
    {
        public event Action OnAddToolEvent;
        public Player Player { get; private set; }
        [field: SerializeField] public SerializedDictionary<PlayerToolType, ToolSO> PlayerTools { get; private set; }
        public ToolSO CurPlayerToolSO { get; private set; }
        
        public void Init(IPlayer player)
        {
            Player = player as Player;
        }

        public override void SetCloth(PlayerToolType type)
        {
            base.SetCloth(type);
            Debug.Log(type.ToString());
            CurPlayerToolSO.UnEquip();
            CurPlayerToolSO = PlayerTools[type];
            CurPlayerToolSO.Equip();
        }

        public void AddTool(ToolSO tool)
        {
            PlayerTools[tool.ToolType] = Instantiate(tool);
            OnAddToolEvent?.Invoke();
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
