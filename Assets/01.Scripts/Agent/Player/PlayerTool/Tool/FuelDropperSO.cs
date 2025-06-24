using JMT.Agent;
using JMT.Item;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    [CreateAssetMenu(fileName = "FuelDropper", menuName = "SO/Data/ToolSO/FuelDropper")]
    // 액체 채집기
    public class FuelDropperSO : ToolSO
    {
        private readonly StatModifier _fuelModifier = new StatModifier(StatModifierType.Percentage, 50);
        public override void Equip()
        {
            base.Equip();
            _player.StatCompo.AddStatModifier(ItemType.LiquidFuel, _fuelModifier);
        }

        public override void UnEquip()
        {
            base.UnEquip();
            _player.StatCompo.RemoveStatModifier(ItemType.LiquidFuel, _fuelModifier);
        }
    }
}