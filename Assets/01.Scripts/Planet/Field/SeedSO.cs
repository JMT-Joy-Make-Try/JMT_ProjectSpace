using AYellowpaper.SerializedCollections;
using JMT.Item;
using UnityEngine;

namespace JMT.Planets.Field
{
    [CreateAssetMenu(fileName = "SeedSO", menuName = "SO/Field/SeedSO")]
    public class SeedSO : ItemSO
    {
        [Header("Seed Properties")]
        public int MaxGrowthStage;
        public GameObject[] SeedObjects;
        

        [Space]
        [Header("Get Item Properties")]
        public SerializedDictionary<ItemSO, int> Items;
    }
}