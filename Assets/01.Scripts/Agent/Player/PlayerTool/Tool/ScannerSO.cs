using JMT.Agent;
using JMT.Item;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    [CreateAssetMenu(fileName = "Scanner", menuName = "SO/Data/ToolSO/Scanner")]
    // 유기물 채집기
    public class ScannerSO : ToolSO
    {
        private readonly StatModifier _organicModifier = new StatModifier(StatModifierType.Percentage, 50);
        public override void Equip()
        {
            base.Equip();
            _player.StatCompo.AddStatModifier(ItemType.Plant, _organicModifier);
        }

        public override void UnEquip()
        {
            base.UnEquip();
            _player.StatCompo.RemoveStatModifier(ItemType.Plant, _organicModifier);
        }
    }
}