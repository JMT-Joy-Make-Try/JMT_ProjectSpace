using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace JMT.Player
{
    public class PlayerTool : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<PlayerToolType, PlayerToolSO> _playerToolSOs;
        private PlayerToolSO _curPlayerToolSO;
        private Player _player;
        
        public void Init(Player player)
        {
            _player = player;
        }
        
        public void SetPlayerTool(PlayerToolType playerToolType)
        {
            _curPlayerToolSO.UnEquip(_player);
            _curPlayerToolSO = _playerToolSOs[playerToolType];
            _curPlayerToolSO.Equip(_player);
        }
    }
    
    public enum PlayerToolType
    {
        DustCollector, // 먼지채집기
        OrganicMatterCollector, // 유기물채집기
        LiquidFuelCollector, // 액체연료채집기
        FilterMask, // 필터마스크
        ProtectiveClothing, // 방호복
    }
}
