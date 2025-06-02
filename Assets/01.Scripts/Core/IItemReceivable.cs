using JMT.Item;

namespace JMT.Core
{
    public interface IItemReceivable
    {
        void ReceiveItem(ItemSO item, int amount);
    }
}