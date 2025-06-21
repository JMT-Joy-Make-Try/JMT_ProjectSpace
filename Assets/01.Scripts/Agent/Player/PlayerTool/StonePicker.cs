using JMT.Agent;
using JMT.Item;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    [CreateAssetMenu(fileName = "StonePicker", menuName = "SO/Data/ToolSO/StonePicker")]
    public class StonePicker : ToolSO
    {
        private readonly StatModifier _stoneModifier = new StatModifier(StatModifierType.Percentage, 50);
        public override void Equip()
        {
            base.Equip();
            _player.StatCompo.AddStatModifier(ItemType.Stone, _stoneModifier);
        }

        public override void UnEquip()
        {
            base.UnEquip();
            _player.StatCompo.RemoveStatModifier(ItemType.Stone, _stoneModifier);
        }
    }
}