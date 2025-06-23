using System.Collections.Generic;
using UnityEngine;

namespace JMT
{
    [CreateAssetMenu(fileName = "VillageList", menuName = "SO/Data/Village/VillageListSO")]
    public class VillageListSO : ScriptableObject
    {
        public List<VillageSO> Villages;
    }
}
