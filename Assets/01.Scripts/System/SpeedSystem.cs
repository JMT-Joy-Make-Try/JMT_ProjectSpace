using System;
using UnityEngine;

namespace JMT
{
    public enum SpeedType
    {
        OneSpeed = 1,
        TwoSpeed = 2,
        ThreeSpeed = 3,
    }
    public class SpeedSystem : MonoSingleton<SpeedSystem>
    {
        public event Action<SpeedType> OnSpeedChangeEvent;
        private SpeedType speedType = SpeedType.OneSpeed;
        public int TimeScale => (int)speedType;
        public void ChangeSpeed()
        {
            switch (speedType)
            {
                case SpeedType.OneSpeed:
                    speedType = SpeedType.TwoSpeed;
                    break;
                case SpeedType.TwoSpeed:
                    speedType = SpeedType.ThreeSpeed;
                    break;
                case SpeedType.ThreeSpeed:
                    speedType = SpeedType.OneSpeed;
                    break;
            }
            Time.timeScale = (int)speedType;
            OnSpeedChangeEvent?.Invoke(speedType);
        }
    }
}
