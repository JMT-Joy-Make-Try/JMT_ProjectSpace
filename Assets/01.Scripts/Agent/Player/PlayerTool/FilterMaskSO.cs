using UnityEngine;

namespace JMT.PlayerCharacter
{
    [CreateAssetMenu(fileName = "FilterMaskSO", menuName = "SO/Data/ToolSO/FilterMaskSO")]
    public class FilterMaskSO : ToolSO
    {
        public override void Equip()
        {
            if (_player.FogDetect.IsPlayerInFog)
                _player.HealthCompo.SetOxygenMultiplier(2);
            else
                _player.HealthCompo.SetOxygenMultiplier(1);
        }

        public override void UnEquip()
        {
            _player.HealthCompo.SetOxygenMultiplier(1);
        }
    }
}