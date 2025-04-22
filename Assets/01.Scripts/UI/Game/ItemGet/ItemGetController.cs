using JMT.Item;
using UnityEngine;

namespace JMT.UISystem.ItemGet
{
    public class ItemGetController : MonoBehaviour
    {
        [SerializeField] private ItemGetView view;

        public void GetItem(ItemSO item, int count)
            => view.GetItem(item, count);
    }
}
