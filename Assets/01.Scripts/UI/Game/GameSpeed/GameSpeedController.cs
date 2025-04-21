using UnityEngine;

namespace JMT.UISystem.GameSpeed
{
    public class GameSpeedController : MonoBehaviour
    {
        [SerializeField] private GameSpeedView view;
        private readonly GameSpeedModel model = new();

        public int TimeScale => (int)model.SpeedType;

        private void Awake()
        {
            view.SetSpeedButtonListener(HandleSpeedButton);
            view.ChangeSpeedText(model.SpeedType);
        }

        private void HandleSpeedButton()
        {
            model.ChangeSpeed();
            view.ChangeSpeedText(model.SpeedType);
        }
    }
}
