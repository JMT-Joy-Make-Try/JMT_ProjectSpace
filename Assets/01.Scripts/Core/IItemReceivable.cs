using JMT.Item;

namespace JMT.Core
{
    public interface IItemReceivable
    {
        bool ReceiveItem(ItemSO item, int amount);
    }
}