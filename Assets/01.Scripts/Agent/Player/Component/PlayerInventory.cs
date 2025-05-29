using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerInventory : MonoBehaviour, IPlayerComponent
    {
        public Player Player { get; private set; }
        public void Init(Player player)
        {
            Player = player;
        }
    }
}