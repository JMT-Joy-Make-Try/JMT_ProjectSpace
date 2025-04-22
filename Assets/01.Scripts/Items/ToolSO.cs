using AYellowpaper.SerializedCollections;
using JMT.Item;
using JMT.Planets.Tile.Items;
using JMT.Player;
using UnityEngine;

namespace JMT
{
    [CreateAssetMenu(fileName = "Tool", menuName = "SO/Data/ToolSO")]
    public class ToolSO : ItemSO
    {
        public SerializedDictionary<ItemSO, int> NeedItems;
        public PlayerToolType ToolType;
        
        public virtual void Equip(Player.Player player)
        {
            
        }
        
        public virtual void UnEquip(Player.Player player)
        {
            
        }
    }
}
