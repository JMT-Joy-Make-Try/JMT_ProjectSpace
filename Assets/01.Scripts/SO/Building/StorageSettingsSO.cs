using JMT.UISystem;
using UnityEngine;

namespace JMT
{
    [CreateAssetMenu(menuName ="SO/Data/Buliding/StorageSO")]
    public class StorageSettingsSO : ScriptableObject
    {
        public CellUI ItemCellUI;

        [Tooltip("총 창고 칸 갯수")]
        public int TotalCellCount;

        [Tooltip("칸 당 최대 아이템 갯수")]
        public int CellItemCount;
    }
}
