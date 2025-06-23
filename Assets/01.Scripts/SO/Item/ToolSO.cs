using AYellowpaper.SerializedCollections;
using JMT.Item;
using JMT.Planets.Tile.Items;
using JMT.PlayerCharacter;
using UnityEngine;

namespace JMT.Item
{
    [CreateAssetMenu(fileName = "Tool", menuName = "SO/Data/Items/ToolSO")]
    public abstract class ToolSO : ItemSO
    {
        public SerializedDictionary<ItemSO, int> NeedItems;
        public PlayerToolType ToolType;
        public bool IsEquipped;
        protected Player _player;
        
        public virtual void Init(IPlayer player)
        {
            _player = player as Player;
            if (_player == null)
            {
                Debug.LogError("Player is not initialized correctly.");
            }
        }

        public virtual void Equip()
        {
            IsEquipped = true;
        }

        public virtual void UnEquip()
        {
            IsEquipped = false;
        }
    }
}
