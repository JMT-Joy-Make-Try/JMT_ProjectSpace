using JMT.UISystem;
using UnityEngine;

namespace JMT.Player
{
    public class PlayerSystem : MonoBehaviour
    {
        [SerializeField] private Player player;

        private PlayerHpUI playerHpUI;

        private void Awake()
        {
            playerHpUI = GetComponent<PlayerHpUI>();
            Debug.Log("네!!!!");
            player.OnDamageEvent += playerHpUI.SetHpBar;
        }
    }
}