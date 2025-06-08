using JMT.Item;
using JMT.PlayerCharacter;
using UnityEngine;

namespace JMT
{
    [CreateAssetMenu(fileName = "Costume", menuName = "SO/Data/Items/CostumeSO")]
    public class CostumeSO : ToolSO
    {
        // public ItemType ItemType;
        public override void Equip()
        {
            base.Equip();
            throw new System.NotImplementedException();
        }

        public override void UnEquip()
        {
            base.UnEquip();
            throw new System.NotImplementedException();
        }
    }
}
