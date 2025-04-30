using UnityEngine;

namespace JMT
{
    [CreateAssetMenu(fileName = "ResourceSO", menuName = "SO/Data/ResourceSO")]
    public class ResourceSO : ScriptableObject
    {
        public int MaxFuelValue;
        public int MaxNpcValue;
    }
}
