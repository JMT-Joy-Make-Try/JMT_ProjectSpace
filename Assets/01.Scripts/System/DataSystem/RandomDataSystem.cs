using UnityEngine;

namespace JMT.DataSystem
{
    public class RandomDataSystem : MonoSingleton<RandomDataSystem>
    {
        [SerializeField] private VillageListSO villageListSO;

        public VillageSO GetRandomVillageSO()
        {
            int random = Random.Range(0, villageListSO.Villages.Count);
            return villageListSO.Villages[random];
        }
    }
}
