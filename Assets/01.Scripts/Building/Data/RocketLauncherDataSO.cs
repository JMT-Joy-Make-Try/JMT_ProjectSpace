using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.Building.Data
{
    [CreateAssetMenu(fileName = "RocketLauncherData", menuName = "SO/RocketLauncherData")]
    public class RocketLauncherDataSO : BuildingDataSO
    {
        [SerializeField] private List<RocketData> _rocketDatas = new List<RocketData>();

        public List<RocketData> RocketDatas => _rocketDatas;
    }

    [System.Serializable]
    public struct RocketData
    {
        public string Name;
        public string Description;
        public Sprite Icon;
    }


}