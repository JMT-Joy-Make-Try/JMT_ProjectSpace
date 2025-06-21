using UnityEngine;

namespace JMT.PlayerCharacter
{
    [CreateAssetMenu(fileName = "SmallBag", menuName = "SO/Data/CostumeSO/SmallBag")]
    public class SmallBag : CostumeSO
    {
        public override void Equip()
        {
            base.Equip();
            _player.InventoryCompo.AddMaxInventorySize(2);
        }
        
        public override void UnEquip()
        {
            base.UnEquip();
            _player.InventoryCompo.RemoveMaxInventorySize(2);
        }
    }
}