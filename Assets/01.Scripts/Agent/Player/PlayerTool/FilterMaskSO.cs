using UnityEngine;

namespace JMT.Player
{
    [CreateAssetMenu(fileName = "FilterMaskSO", menuName = "SO/Data/ToolSO/FilterMaskSO")]
    public class FilterMaskSO : ToolSO
    {
        public override void Equip(Player player)
        {
            if (player.FogDetect.IsPlayerInFog)
                player.SetOxygenMultiplier(2);
            else
                player.SetOxygenMultiplier(1);
        }

        public override void UnEquip(Player player)
        {
            player.SetOxygenMultiplier(1);
        }
    }
}