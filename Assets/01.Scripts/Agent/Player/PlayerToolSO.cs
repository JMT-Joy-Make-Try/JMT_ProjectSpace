using UnityEngine;

namespace JMT.Player
{
    public abstract class PlayerToolSO : ScriptableObject
    {
        public abstract void Equip(Player player);
        public abstract void UnEquip(Player player);
    }
}