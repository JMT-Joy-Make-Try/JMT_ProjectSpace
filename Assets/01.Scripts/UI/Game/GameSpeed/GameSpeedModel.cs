using UnityEngine;

namespace JMT.UISystem.GameSpeed
{
    public enum SpeedType
    {
        OneSpeed = 1,
        TwoSpeed = 2,
        ThreeSpeed = 3,
    }
    public class GameSpeedModel
    {
        private SpeedType speedType = SpeedType.OneSpeed;
        public SpeedType SpeedType => speedType;
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
        }
    }
}
