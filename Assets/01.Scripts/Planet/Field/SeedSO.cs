using JMT.Item;
using UnityEngine;

namespace JMT.Planets.Field
{
    [CreateAssetMenu(fileName = "SeedSO", menuName = "SO/Field/SeedSO")]
    public class SeedSO : ItemSO
    {
        public SeedType SeedType;
    }

    public enum SeedType
    {
        
    }
}