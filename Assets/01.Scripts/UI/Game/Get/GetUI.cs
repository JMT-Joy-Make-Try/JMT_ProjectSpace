using JMT.Item;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.UISystem
{
    public class GetUI : MonoBehaviour
    {
        [SerializeField] private Transform cellParent;
        [SerializeField] private GetCellUI cellUIPrefab;

        public void GetItem(ItemSO item, int count)
        {
            GetCellUI cellUI = Instantiate(cellUIPrefab, cellParent);
            cellUI.SetData(item, count);
        }
    }
}
