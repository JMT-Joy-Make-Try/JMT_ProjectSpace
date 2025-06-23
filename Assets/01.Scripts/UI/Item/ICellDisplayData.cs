using UnityEngine;

namespace JMT
{
    public interface ICellDisplayData
    {
        Sprite DisplayIcon { get; }
        string DisplayName { get; }
    }
}
