using JMT.Item;
using UnityEngine;

namespace JMT.UISystem.ItemGet
{
    public class ItemGetView : MonoBehaviour
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
