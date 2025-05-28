using UnityEngine;

namespace JMT.PlayerCharacter
{
    [CreateAssetMenu(fileName = "FilterMaskSO", menuName = "SO/Data/ToolSO/FilterMaskSO")]
    public class FilterMaskSO : ToolSO
    {
        public override void Equip(Player player)
        {
            if (player.FogDetect.IsPlayerInFog)
                player.HealthCompo.SetOxygenMultiplier(2);
            else
                player.HealthCompo.SetOxygenMultiplier(1);
        }

        public override void UnEquip(Player player)
        {
            player.HealthCompo.SetOxygenMultiplier(1);
        }
    }
}