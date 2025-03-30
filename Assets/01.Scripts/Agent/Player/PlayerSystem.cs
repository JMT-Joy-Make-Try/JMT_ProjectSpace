using JMT.UISystem;
using UnityEngine;

namespace JMT.Player
{
    public class PlayerSystem : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private PlayerFillUI playerHpUI;
        [SerializeField] private PlayerFillUI playerOxygenUI;


        private void Awake()
        {
            player.OnDamageEvent += playerHpUI.SetHpBar;
            player.OnOxygenEvent += playerOxygenUI.SetHpBar;
        }
    }
}