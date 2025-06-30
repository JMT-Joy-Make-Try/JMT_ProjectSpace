using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Item;
using System;
using UnityEngine;

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
            Init(PlayerToolType.None);
        }

        public override void SetCloth(PlayerToolType type)
        {
            if (CurPlayerToolSO?.ToolType == type)
            {
                Debug.LogWarning($"Already equipped tool: {type}");
                return;
            }
            base.SetCloth(type);
            Debug.Log(type.ToString());
            agentClothList[PlayerToolType.None].gameObject.SetActive(true);
            CurPlayerToolSO?.UnEquip();
            CurPlayerToolSO = PlayerTools[type];
            CurPlayerToolSO.Equip();
        }

        public void AddTool(ToolSO tool)
        {
            PlayerTools[tool.ToolType] = Instantiate(tool);
            PlayerTools[tool.ToolType].Init(Player);
            OnAddToolEvent?.Invoke();
        }
        
        public bool IsEquippedTool(PlayerToolType toolType)
        {
            var tool = CurPlayerToolSO;
            if (tool == null) return false;
            return tool.ToolType == toolType;
        }

        public void UnEquipTool(PlayerToolType toolType)
        {
            if (CurPlayerToolSO == null || CurPlayerToolSO.ToolType != toolType)
            {
                Debug.LogWarning($"No tool equipped of type: {toolType}");
                return;
            }
            CurPlayerToolSO.UnEquip();
            CurPlayerToolSO = null;
            SetCloth(PlayerToolType.None);
        }
    }
    
    public enum PlayerToolType
    {
        None,
        Vacuum, // 먼지채집기
        Scanner, // 유기물채집기
        FuelDropper, // 액체연료채집기
        FilterMask, // 필터마스크
        Hammer, // 망치
        Farmer, // 농기구
    }
}
